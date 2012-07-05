`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date:    12:30:51 02/18/2009 
// Design Name: 
// Module Name:    AvivFPGA2 
// Project Name: 
// Target Devices: 
// Tool versions: 
// Description: 
//
// Dependencies: 
//
// Revision: 
// Revision 0.01 - File Created
// Additional Comments: 
//
//////////////////////////////////////////////////////////////////////////////////
module AvivFPGA2(
	input  wire [7:0]  hi_in,
   output wire [1:0]  hi_out,
   inout  wire [15:0] hi_inout,
   input  wire clk1,
	input  wire clk2,
   output wire [7:0]  led,
	input wire hard_trig,
	inout wire [0:35] ybus,
	input wire yclk1);


// Opal Kelly interface bus:
wire         ti_clk;         // clock for PC communication. Used in the input side of the FIFO
wire [30:0]  ok1;				  // Opal Kelly bus 1
wire [16:0]  ok2;            // Opal Kelly bus 2

// the mapping to clk1 is temporary. Eventually, we will map the refclk to an input pin.
wire refclk;
assign refclk = clk1;      // ********************** UNCOMMENT THIS LINE TO USE INTERNAL CLOCK
//assign refclk = yclk1;   // ********************** UNCOMMENT THIS LINE TO USE EXTERNAL CLOCK


// Pipe related wires
wire pipeI_write;
wire [15:0] pipeI_data;


// Wires related to FIFO
reg fifo_read_enable;
reg fifo_reset;
wire [127 : 0] fifo_dout;
wire empty;
wire full;
wire overflow;
wire underflow;

// create a local register for output clock. And tie it to the output pin (which is currently not assigned to a physical pin)
reg output_clock;
wire outclk;
assign outclk=output_clock;
assign ybus[25] = output_clock;
assign ybus[24] = output_clock;
assign ybus[23] = output_clock;
assign ybus[22] = output_clock;

assign ybus[26] = ~output_clock;

reg [31:0] masterSamplesGenerated;
reg [31:0] retriggerWaitSamples;   // Counts the total number of samples spent waiting for retriggers


		// register inputs from computer
wire [15:0] ok_wire_ins;

	// input from computer -- specifies how long to count up to for debounce counter
wire [15:0] debounce_counts;
reg  [15:0] debounce_counter;

// modulated clock signal... this will be interesting
wire use_modulated_clock;
assign use_modulated_clock = ok_wire_ins[1];
assign ybus[13] = output_clock & clk2 & use_modulated_clock;

// Possible generation states
parameter s_idle = 1,
          s_preparing_to_generate=2,
          s_generating=3;

// Register for holding our current state.
reg [4:0] state; // this is 4 bits wide even though only 2 bits are used. I think the compiler shrinks it to 2 anyway.


// 1-bit register used to keep track of if we are actively waiting for retrigger
// 32-bit register used to count how long we have waited for
reg waitingForRetrigger;
reg [31:0] waitedCounts;
reg [15:0] retriggerTimeoutCount; 

reg [3:0] nSegsGenerated; // used only to drive LEDs


// bit 0: last run finished successfully
// bit 1: last run was aborted
// bits 2-15: reserved for future use
reg[15:0] fpgaStatusOut;


// Mistrigger detection stuff

reg [31:0] mistriggerDetected;
reg [31:0] nSamplesGenerated;
reg toggler;
wire togglerIN;
assign togglerIN = ybus[0];
assign ybus[1] = toggler;

wire retriggerIN_bouncy;
assign retriggerIN_bouncy = ybus[2];


// Retrigger debounce stuff
reg retriggerIN;
reg retrigger_next;
always @(posedge refclk) begin
	retrigger_next<=retriggerIN_bouncy;
	if (retrigger_next==retriggerIN_bouncy) begin
		debounce_counter<=debounce_counter+1;
		if (debounce_counter>=debounce_counts) begin
			retriggerIN<=retrigger_next;
		end
	end
	else begin
		debounce_counter<=0;  // reset the debounce counter every time the bouncy input chages state
	end
end

wire hard_trig_in;

		//  trigger inputs from computer
wire [15:0] ok_trig_ins;

wire soft_generate_trig_in;
wire soft_abort_trig_in;

assign soft_generate_trig_in = ok_trig_ins[0];
assign soft_abort_trig_in =    ok_trig_ins[1];

wire use_hard_trig;
assign use_hard_trig = ok_wire_ins[0];


reg [48:0] main_counter;
reg [32:0] repeat_counter; 
reg internal_reset;


reg lastRetriggerIn;

// Initialization of state machine
initial begin
	state<=s_idle;
	main_counter<=0;
	repeat_counter<=0;
	nSegsGenerated<=0;
	mistriggerDetected<=0;
	masterSamplesGenerated<=0;
	retriggerTimeoutCount<=0;
	retriggerWaitSamples<=0;
	fpgaStatusOut<=0;
	lastRetriggerIn<=0;
end

// triggering logic
reg hard_triggered;


always @(posedge hard_trig_in) begin
	hard_triggered<=1;
end


reg [127:0] clock_data_from_fifo; // local register in which we will store data from the FIFO

wire [47:0] on_counts;				// FIFO data contains 3 important numbers
wire [47:0] off_counts;
wire [31:0] repeat_counts;

wire edgeRetrigger = off_counts[0];
wire retriggerValue = off_counts[1];



												// divide the FIFO data correctly into those 3 numbers
assign on_counts = clock_data_from_fifo[127:80];
assign off_counts = clock_data_from_fifo[79:32];
assign repeat_counts = clock_data_from_fifo[31:0];


wire negclk;								// create a negative of our reference clock. This is used to clock the FIFO
assign negclk = ~refclk;



always @(posedge refclk) begin
	lastRetriggerIn<=retriggerIN;

	// read from FIFO into local register, when required
	if (fifo_read_enable==1) begin
		fifo_read_enable<=0;
		clock_data_from_fifo=fifo_dout;
	end
	
	// clear fifo_reset bit if neccesary
	if (fifo_reset==1) begin
		fifo_reset<=0;
	end


	// main state machine
	case (state)
		s_idle: begin // idle state: reset registers to idle values, wait for soft trigger to begin generation.
			output_clock<=0;
			main_counter<=0;
			repeat_counter<=0;
			nSegsGenerated<=0;
			nSamplesGenerated<=0;
			waitingForRetrigger<=0;
			waitedCounts<=0;
			toggler<=0;

			if (soft_generate_trig_in==1) begin
				if (fifo_read_enable==0) begin
					state<=s_preparing_to_generate; 
					fifo_read_enable<=1;               // read the first list item from the FIFO before starting generation
				end
			end
		end
		
		s_preparing_to_generate: begin			// this state lasts for only 1 clock cycle, to allow the first frame to be read from the FIFO
			state<=s_generating;					// and then we immediately enter the generating state.
			mistriggerDetected<=0;  			// we also use this opportunity to reset the mistriggerDetected register.
			fpgaStatusOut<=0;						// and reset the status flags
			masterSamplesGenerated<=0;       // and reset the previous master sample generation count
			retriggerWaitSamples<=0;
			retriggerTimeoutCount<=0;
		end
		
		s_generating: begin							// generating state
			if (soft_abort_trig_in==1) begin    // handle an abort trigger
				state<=s_idle;							// return to idle state
				fifo_reset<=1;							// reset FIFO
				output_clock<=0;						// and clock
				fpgaStatusOut[1]<=1;					// And mark the status bit accordingly
			end
			else if (repeat_counts==0) begin
																	// repeat_counts == 0 is a special code for
																	// putting the FPGA into "wait for retrigger" mode
																	// in this mode, if on_counts!=0, then on_counts 
																	// is actually the wait timeout, after which we should 
																	// continue even if we don't receieve the retrigger signal
				if (waitingForRetrigger) begin
					waitedCounts<=waitedCounts+1;
					 retriggerWaitSamples<=retriggerWaitSamples+1;
					if ((!edgeRetrigger && retriggerIN == retriggerValue && fifo_read_enable==0) // wait for retrigger input to go to correct value
					   ||(edgeRetrigger && retriggerIN == retriggerValue && retriggerIN!=lastRetriggerIn && fifo_read_enable==0)// or for it to have edge of correct value
					|| (on_counts!=0 && waitedCounts==on_counts))  //or for the wait to timeout, then move on in the fifo
					begin		
						fifo_read_enable<=1;									// this will cause us to move on in the fifo
						nSegsGenerated<=nSegsGenerated+1;				// let's also update status counters
						waitingForRetrigger<=0;
						if (on_counts!=0 && waitedCounts==on_counts)
							retriggerTimeoutCount<=retriggerTimeoutCount+1;
					end
				end
				else begin
					waitingForRetrigger<=1;
					waitedCounts<=0;
				end
			end
			else begin		
				masterSamplesGenerated<=masterSamplesGenerated+1; // increment the master sample counter
				if (main_counter<on_counts) begin   // if the counter indicates our output should be high
					if (output_clock==0) begin          // then if it is low
						output_clock<=1;					 	// make it high
						nSamplesGenerated<=nSamplesGenerated+1; // and increment the samples generated count
						toggler<=~toggler; 							 // and toggle the toggler
						if (toggler!=togglerIN) begin 			 // and check if the togger matched the toggler input						
							if (mistriggerDetected==0) begin
								mistriggerDetected<=nSamplesGenerated; // note that this will store 1 sample later than any detected mistrig.
							end
						end
					end	
					main_counter<=main_counter+1;		// finally increment counter                
				end
				else begin
					if (main_counter<(on_counts+off_counts-1)) begin // otherwise if the counter should be be low
						output_clock<=0;									// make it low
						main_counter<=main_counter+1;					// and increment the counter
					end
					else begin												// otherwise we have reached the end of 1 period
						main_counter<=0;									// so reset counter
						output_clock<=0;									// and put the clock low
						if (repeat_counter<repeat_counts-1) begin	// if we should repeat at this frequency
							repeat_counter<=repeat_counter+1;		// then increment the repeat counter
						end
						else begin											// otherwise
							repeat_counter<=0;							// reset the repeat counter
							if (empty==0) begin							// and if there is data left in the FIFO
								if (fifo_read_enable==0) begin
									fifo_read_enable<=1;						// read the next frame
									nSegsGenerated<=nSegsGenerated+1;
								end
							end
							else begin										// no data in the FIFO?
								state<=s_idle;								// then we are done. go back to idle
								fifo_reset<=1;								// this line is probably unnecessary
								fpgaStatusOut[0]<=1; 					// Mark the status output that we are done.
							end
						end
					end		
				end			
			end
		end
	endcase
end


// Create Opal Kelly Host Interfaces for communication with PC

okHostInterface okHI(.hi_in(hi_in), .hi_out(hi_out), .hi_inout(hi_inout),
	.ti_clk(ti_clk), .ok1(ok1), .ok2(ok2));

okPipeIn ep80 (.ok1(ok1), .ok2(ok2),
	.ep_addr(8'h80), .ep_write(pipeI_write), .ep_dataout(pipeI_data));
	
	
okTriggerIn trigIn40 (.ok1(ok1), .ok2(ok2),
	.ep_clk(refclk), .ep_addr(8'h40), .ep_trigger(ok_trig_ins));

okWireIn wireIn00 (.ok1(ok1), .ok2(ok2),
	.ep_addr(8'h00), .ep_dataout(ok_wire_ins));
	
okWireIn wireIn01 (.ok1(ok1), .ok2(ok2),
	.ep_addr(8'h01), .ep_dataout(debounce_counts));


// Wire outs for mistrigger detection
okWireOut wire20 (.ok1(ok1), .ok2(ok2), .ep_addr(8'h20), .ep_datain(mistriggerDetected[15:0]));
okWireOut wire21 (.ok1(ok1), .ok2(ok2), .ep_addr(8'h21), .ep_datain(mistriggerDetected[31:16]));

// Wire outs for Atticus polling of FPGA's master sample count
okWireOut wire22 (.ok1(ok1), .ok2(ok2), .ep_addr(8'h22), .ep_datain(masterSamplesGenerated[15:0]));
okWireOut wire23 (.ok1(ok1), .ok2(ok2), .ep_addr(8'h23), .ep_datain(masterSamplesGenerated[31:16]));

// Wire out for Atticus polling of # of retriggers that timed out
okWireOut wire24 (.ok1(ok1), .ok2(ok2), .ep_addr(8'h24), .ep_datain(retriggerTimeoutCount[15:0]));

// Wire out for Atticus polling of fpga status
okWireOut wire25 (.ok1(ok1), .ok2(ok2), .ep_addr(8'h25), .ep_datain(fpgaStatusOut[15:0]));

// Wire out for Atticus polling of # of samples spent waiting for retrigger
okWireOut wire26 (.ok1(ok1), .ok2(ok2), .ep_addr(8'h26), .ep_datain(retriggerWaitSamples[15:0]));
okWireOut wire27 (.ok1(ok1), .ok2(ok2), .ep_addr(8'h27), .ep_datain(retriggerWaitSamples[31:16]));

// Create the FIFO for storing data from computer
// write clock comes from Opal Kelly Host Interface
// as does write data and write enable. 
// Note that the FIFO is clocked on the negative clock cycles, to allow us to 
// read from the FIFO in between our counter increments

TestFifo ClockSegmentsFifo(
	pipeI_data,
	negclk,
	fifo_read_enable,
	fifo_reset,
	ti_clk,
	pipeI_write,
	fifo_dout,
	empty,
	full,
	overflow,
	underflow);
	
	

// Visual feedback of device state:
assign led[3:0] = ~nSegsGenerated[3:0];
assign led[5:4] = ~state[1:0];
assign led[6] = ~empty;
assign led[7] = ~outclk;
	

endmodule
