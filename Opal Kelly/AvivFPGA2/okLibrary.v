//------------------------------------------------------------------------
// FrontPanel Library Module Declarations (Verilog)
//
// Copyright (c) 2004-2006 Opal Kelly Incorporated
// $Rev: 74 $ $Date: 2005-11-07 09:44:15 -0600 (Mon, 07 Nov 2005) $
//------------------------------------------------------------------------

module okHostInterface(hi_in, hi_out, hi_inout, ti_clk, ok1, ok2);
	input  wire [7:0]  hi_in;
	output wire [1:0]  hi_out;
	inout  wire [15:0] hi_inout;
	output wire        ti_clk;
	output wire [30:0] ok1;
	input  wire [16:0] ok2;

	wire [15:0] hi_datain;
	wire [15:0] hi_dataout;
	wire [2:0]  hi_out_core;
	wire        hi_drive_b = ~hi_out_core[2];
	
	// Clock buffer for the Host Interface clock.
	BUFGDLL clkbuf(.I(hi_in[0]), .O(ti_clk));
	
	// Instantiate bidirectional IOBUFs for the hi_data lines.
	IOBUF iobuf0(.T(hi_drive_b),  .O(hi_datain[0]),  .I(hi_dataout[0]),  .IO(hi_inout[0]));
	IOBUF iobuf1(.T(hi_drive_b),  .O(hi_datain[1]),  .I(hi_dataout[1]),  .IO(hi_inout[1]));
	IOBUF iobuf2(.T(hi_drive_b),  .O(hi_datain[2]),  .I(hi_dataout[2]),  .IO(hi_inout[2]));
	IOBUF iobuf3(.T(hi_drive_b),  .O(hi_datain[3]),  .I(hi_dataout[3]),  .IO(hi_inout[3]));
	IOBUF iobuf4(.T(hi_drive_b),  .O(hi_datain[4]),  .I(hi_dataout[4]),  .IO(hi_inout[4]));
	IOBUF iobuf5(.T(hi_drive_b),  .O(hi_datain[5]),  .I(hi_dataout[5]),  .IO(hi_inout[5]));
	IOBUF iobuf6(.T(hi_drive_b),  .O(hi_datain[6]),  .I(hi_dataout[6]),  .IO(hi_inout[6]));
	IOBUF iobuf7(.T(hi_drive_b),  .O(hi_datain[7]),  .I(hi_dataout[7]),  .IO(hi_inout[7]));
	IOBUF iobuf8(.T(hi_drive_b),  .O(hi_datain[8]),  .I(hi_dataout[8]),  .IO(hi_inout[8]));
	IOBUF iobuf9(.T(hi_drive_b),  .O(hi_datain[9]),  .I(hi_dataout[9]),  .IO(hi_inout[9]));
	IOBUF iobuf10(.T(hi_drive_b), .O(hi_datain[10]), .I(hi_dataout[10]), .IO(hi_inout[10]));
	IOBUF iobuf11(.T(hi_drive_b), .O(hi_datain[11]), .I(hi_dataout[11]), .IO(hi_inout[11]));
	IOBUF iobuf12(.T(hi_drive_b), .O(hi_datain[12]), .I(hi_dataout[12]), .IO(hi_inout[12]));
	IOBUF iobuf13(.T(hi_drive_b), .O(hi_datain[13]), .I(hi_dataout[13]), .IO(hi_inout[13]));
	IOBUF iobuf14(.T(hi_drive_b), .O(hi_datain[14]), .I(hi_dataout[14]), .IO(hi_inout[14]));
	IOBUF iobuf15(.T(hi_drive_b), .O(hi_datain[15]), .I(hi_dataout[15]), .IO(hi_inout[15]));

	OBUF obuf0(.I(hi_out_core[0]), .O(hi_out[0]));
	OBUF obuf1(.I(hi_out_core[1]), .O(hi_out[1]));

	// Instantiate the core Host Interface.
	okHostInterfaceCore hicore(.hi_in({hi_in[7:1], ti_clk}), .hi_out(hi_out_core),
				.hi_datain(hi_datain), .hi_dataout(hi_dataout),
				.ok1(ok1), .ok2(ok2));
endmodule


module okHostInterfaceCore(hi_in, hi_out, hi_datain, hi_dataout, ok1, ok2);
	input  [7:0]  hi_in;
	output [2:0]  hi_out;
	input  [15:0] hi_datain;
	output [15:0] hi_dataout;
	output [30:0] ok1;
	input  [16:0] ok2;
// synthesis attribute box_type okHostInterfaceCore "black_box"
endmodule


module okWireIn(ok1, ok2, ep_addr, ep_dataout);
	input  [30:0] ok1;
	output [16:0] ok2;
	input  [7:0]  ep_addr;
	output [15:0] ep_dataout;
// synthesis attribute box_type okWireIn "black_box"
endmodule


module okWireOut(ok1, ok2, ep_addr, ep_datain);
	input  [30:0] ok1;
	output [16:0] ok2;
	input  [7:0]  ep_addr;
	input  [15:0] ep_datain;
// synthesis attribute box_type okWireOut "black_box"
endmodule


module okTriggerIn(ok1, ok2, ep_addr, ep_clk, ep_trigger);
	input  [30:0] ok1;
	output [16:0] ok2;
	input  [7:0]  ep_addr;
	input         ep_clk;
	output [15:0] ep_trigger;
// synthesis attribute box_type okTriggerIn "black_box"
endmodule


module okTriggerOut(ok1, ok2, ep_addr, ep_clk, ep_trigger);
	input  [30:0] ok1;
	output [16:0] ok2;
	input  [7:0]  ep_addr;
	input         ep_clk;
	input  [15:0] ep_trigger;
// synthesis attribute box_type okTriggerOut "black_box"
endmodule


module okPipeIn(ok1, ok2, ep_addr, ep_write, ep_dataout);
	input  [30:0] ok1;
	output [16:0] ok2;
	input  [7:0]  ep_addr;
	output        ep_write;
	output [15:0] ep_dataout;
// synthesis attribute box_type okPipeIn "black_box"
endmodule


module okPipeOut(ok1, ok2, ep_addr, ep_read, ep_datain);
	input  [30:0] ok1;
	output [16:0] ok2;
	input  [7:0]  ep_addr;
	output        ep_read;
	input  [15:0] ep_datain;
// synthesis attribute box_type okPipeOut "black_box"
endmodule

module okBTPipeIn(ok1, ok2, ep_addr, ep_write, ep_blockstrobe, ep_dataout, ep_ready);
	input  [30:0] ok1;
	output [16:0] ok2;
   input  [7:0]  ep_addr;
   output        ep_write;
	output        ep_blockstrobe;
   output [15:0] ep_dataout;
   input         ep_ready;
// synthesis attribute box_type okBTPipeIn "black_box"
endmodule


module okBTPipeOut(ok1, ok2, ep_addr, ep_read, ep_blockstrobe, ep_datain, ep_ready);
	input  [30:0] ok1;
	output [16:0] ok2;
	input  [7:0]  ep_addr;
	output        ep_read;
	output        ep_blockstrobe;
	input  [15:0] ep_datain;
	input         ep_ready;
// synthesis attribute box_type okBTPipeOut "black_box"
endmodule

