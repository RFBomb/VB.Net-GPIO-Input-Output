Imports Windows.Devices.Gpio

Module GPIO_Setup

    'This Module Sets up the Raspberry Pi with 4 inputs and 4 outputs

    'Input Pins
    'GPIO_Input_1 =  GPIO 2 = Pin 3    
    'GPIO_Input_2 =  GPIO 3 = Pin 5
    'GPIO_Input_3 =  GPIO 4 = Pin 7
    'GPIO_Input_4 =  GPIO 17 = Pin 11

    'Output Pins
    'GPIO_Output_1 = GPIO 5 = Pin 29
    'GPIO_Output_2 = GPIO 6 = Pin 31
    'GPIO_Output_3 = GPIO 13 = Pin 33
    'GPIO_Output_4 = GPIO 19 = Pin 35

    Private Gpio = GpioController.GetDefault
    'Inputs
    Public GPIO_Input_1 As GpioPin = Gpio.OpenPin(2)
    Public GPIO_Input_2 As GpioPin = Gpio.OpenPin(3)
    Public GPIO_Input_3 As GpioPin = Gpio.OpenPin(4)
    Public GPIO_Input_4 As GpioPin = Gpio.OpenPin(17)
    'Outputs
    Public GPIO_Output_1 As GpioPin = Gpio.OpenPin(5)
    Public GPIO_Output_2 As GpioPin = Gpio.OpenPin(6)
    Public GPIO_Output_3 As GpioPin = Gpio.OpenPin(13)
    Public GPIO_Output_4 As GpioPin = Gpio.OpenPin(19)

    Public Sub Initialize_GPIO()
        'Set GPIO Inputs
        Call SetGPIO_Inputs(GPIO_Input_1)
        Call SetGPIO_Inputs(GPIO_Input_2)
        Call SetGPIO_Inputs(GPIO_Input_3)
        Call SetGPIO_Inputs(GPIO_Input_4)
        'Set GPIO Outputs
        Call SetGPIO_Outputs(GPIO_Output_1)
        Call SetGPIO_Outputs(GPIO_Output_2)
        Call SetGPIO_Outputs(GPIO_Output_3)
        Call SetGPIO_Outputs(GPIO_Output_4)
    End Sub

    Private Sub SetGPIO_Inputs(GPIO_Input As GpioPin)
        GPIO_Input.SetDriveMode(GpioPinDriveMode.InputPullDown)
        GPIO_Input.SetDriveMode(GpioPinEdge.RisingEdge)
    End Sub

    Private Sub SetGPIO_Outputs(GPIO_Output As GpioPin)
        GPIO_Output.SetDriveMode(GpioPinDriveMode.Output)
        GPIO_Output.Write(GpioPinValue.Low)
    End Sub

    ''' 
    ''' This Section toggles the state of the Output Pins
    ''' 

    Public Sub SetOutputHigh(GPIO_Output As GpioPin)
        GPIO_Output.Write(GpioPinValue.High)
    End Sub

    Public Sub SetOutputLow(GPIO_Output As GpioPin)
        GPIO_Output.Write(GpioPinValue.Low)
    End Sub

    Public Sub ToggleOutput(GPIO_Output As GpioPin)
        'Read pin status and invert state
        If GPIO_Output.Read = GpioPinValue.High Then
            GPIO_Output.Write(GpioPinValue.Low)
        Else
            GPIO_Output.Write(GpioPinValue.High)
        End If
    End Sub

    ''' 
    ''' This Section evaluates Inputs Pins
    ''' 

    Public Sub Wait_GPIO_High(GPIO_Input As GpioPin)
        While GPIO_Input.Read = GpioPinValue.Low
            'Do Nothing
        End While
    End Sub

    Public Sub Wait_GPIO_Low(GPIO_Input As GpioPin)
        While GPIO_Input.Read = GpioPinValue.High
            'Do Nothing
        End While
    End Sub

    Public Sub Wait_GPIO_StateChange(GPIO_Input As GpioPin)
        If GPIO_Input.Read = GpioPinValue.High Then Call Wait_GPIO_Low(GPIO_Input) Else Call Wait_GPIO_High(GPIO_Input)
    End Sub

    Public Sub Evaluate_GPIO_Input(GPIO_Input As GpioPin, PinValue As Boolean)
        If GPIO_Input.Read = GpioPinValue.High Then
            PinValue = True
        Else
            PinValue = False
        End If
    End Sub

End Module
