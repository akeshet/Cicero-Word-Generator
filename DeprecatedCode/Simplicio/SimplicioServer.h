#pragma once
//#using <DataStructures.dll>
namespace forms2{
	using namespace DataStructures;
	using namespace System;
	using namespace System::Collections::Generic;
	ref class Form1;
	public ref class SimplicioServer : public ServerCommunicator{
	public:

		SimplicioServer(Form1^ f);
		void connect();

        virtual bool armTasks() override;
		virtual BufferGenerationStatus generateBuffers(int listIterationNumber) override;
        virtual bool generateTrigger() override;
		virtual List<HardwareChannel>^ getHardwareChannels() override;
		virtual String^ getServerName() override;
		virtual ServerSettingsInterface^ getServerSettings() override;
		virtual void nextRunTimeStamp(DateTime timeStamp) override;
		virtual bool outputGPIBGroup(GPIBGroup^ gpibGroup, SettingsData^ settings) override;
		virtual bool outputRS232Group(RS232Group^ rs232Group, SettingsData^ settings) override;
        virtual bool outputSingleTimestep(SettingsData^ settings, SingleOutputFrame^ output) override;
        virtual bool ping() override;
        virtual bool runSuccess() override;
        virtual bool setSequence(SequenceData^ sequence) override;
		virtual bool setSettings(SettingsData^ settings) override;
        virtual void stop() override;

	private:
		delegate void DelegateVars(LinkedList<Variable^>^);
		delegate void DelegateTime(DateTime);

		Form1^ mainForm;
		DelegateTime^ setNextTimeMainForm;
		DelegateVars^ seqStartedMainForm;
		//DateTime nextRunTime;
	};
}