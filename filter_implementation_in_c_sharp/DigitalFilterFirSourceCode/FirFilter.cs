using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalFirFilterDesign
{
    public class FirFilter
    {
        // This impulse response is for a 8-20 Hz BandPass Filter, where sampling frequency (fs) is 128 Hz
        double[] impulseResponse = {0.004528172943153327,0.001286301730457399,-0.01766075085215446,-0.0627815340506953,
            -0.10435600895817357,-0.07876577665032439,0.042466738112412694,0.19610237607821238,
            0.2667314034823978,0.19610237607821238,0.042466738112412694,-0.07876577665032443,
            -0.10435600895817357,-0.06278153405069527,-0.01766075085215446,0.0012863017304574004,
            0.004528172943153327};

        double[] signalBuffer;
        int bufferIndex;

        public FirFilter()
        {
            // The signal buffer array is necessary for the calculation. Initially all the values are set to zero, and
            // with input signals, the values from the signals fill it up.
            signalBuffer = new double[impulseResponse.Length];

            for (int j = 0; j < signalBuffer.Length; j++)
            {
                signalBuffer[j] = 0;
            }

            bufferIndex = 0;
        }

        public double[] FilterSignal(double[] signal)
        {
            double[] outputValues = new double[signal.Length];

            for (int i = 0; i < signal.Length; i++)
            {
                outputValues[i] = continuousFilter(signal[i]);
            }

            return outputValues;
        }

        private double continuousFilter(double inputSignalSample)
        {
            // Store latest sample in circular buffer
            signalBuffer[bufferIndex] = inputSignalSample;

            // Increment buffer index and wrap if necessary
            bufferIndex++;

            if (bufferIndex == signalBuffer.Length)
            {
                bufferIndex = 0;
            }

            // Compute the new output by convolution
            double outValue = 0;

            int operationIndex = bufferIndex;

            for (int i = 0; i < impulseResponse.Length; i++)
            {
                if (operationIndex > 0)
                {
                    operationIndex--;
                }
                else
                {
                    operationIndex = impulseResponse.Length - 1;
                }

                outValue += (impulseResponse[i] * signalBuffer[operationIndex]);
            }

            return outValue;
        }
    }
}
