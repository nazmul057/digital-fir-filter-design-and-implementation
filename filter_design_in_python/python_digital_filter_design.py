from scipy import signal

def iir_filter_design():
    # this is for iir filter design
    b, a = signal.butter(2, [40/500, 60/500], 'bandpass')
    print(b)
    print(a)

def fir_filter_design():
    # numtaps must be odd if the nyquist frequency is in the pass band
    # numtaps = order of filter + 1

    filter_coefficients = signal.firwin(numtaps=17, cutoff=[8, 20], pass_zero='bandpass', fs=128)
    # The line above is for a bandpass filter with cutoff at 8 Hz and 20 Hz, where sampling frequency is 128 Hz.
    
    # firwin input for some other filters are given below
    # filter_coefficients = signal.firwin(numtaps=17, cutoff=20, pass_zero='lowpass', fs=1000) # lowpass with cutoff at 20 Hz, fs = 1000 Hz
    # filter_coefficients = signal.firwin(numtaps=17, cutoff=20, pass_zero='highpass', fs=128) # highpass with cutoff at 20 Hz, fs = 128 Hz
    # filter_coefficients = signal.firwin(numtaps=17, cutoff=[45, 55], pass_zero='bandstop', fs=1000) # bandpass for 45-55 Hz cutoff, fs = 1000 Hz

    # for more information, visit python scipy signal documentation

    # These filter_coefficients will be needed in c# code for filter calculations. The next part just outputs the coefficients in a test file from where
    # they can be easily copied and pasted.

    filter_coefficients_string = "Output: {"

    newLineFlag = 0

    for (i, v) in enumerate(filter_coefficients):
        filter_coefficients_string += str(v)
        
        if (i < (len(filter_coefficients) - 1)):
            filter_coefficients_string += ","

        newLineFlag += 1

        if newLineFlag == 8:
            filter_coefficients_string += "\n"
            newLineFlag = 0
    
    filter_coefficients_string += "}"
    
    f = open("fir_filter_coefficients.txt", "w")
    f.write(filter_coefficients_string)
    f.close()

    print(filter_coefficients)

fir_filter_design()