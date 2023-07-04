using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace DigitalFirFilterDesign
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PlotModel PlotModelInputSignal { get; private set; }
        public PlotModel PlotModelOutputSignal { get; private set; }

        public LineSeries LsFilterIn { get; private set; }
        public LineSeries LsFilterOut { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            #region plotmodels

            // Input Signal Plot Setup
            PlotModelInputSignal = new PlotModel();
            PlotModelInputSignal.Background = OxyColors.White;

            PlotModelInputSignal.Axes.Add(new LinearAxis
            {
                Title = "Y Axis",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Left
            });

            PlotModelInputSignal.Axes.Add(new LinearAxis
            {
                Title = "X Axis (Input Signal)",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Bottom
            });

            LsFilterIn = new LineSeries();

            PlotModelInputSignal.Series.Add(LsFilterIn);

            // Output Signal Plot Setup
            PlotModelOutputSignal = new PlotModel();

            PlotModelOutputSignal.Background = OxyColors.White;

            PlotModelOutputSignal.Axes.Add(new LinearAxis
            {
                Title = "Y Axis",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Left
            });

            PlotModelOutputSignal.Axes.Add(new LinearAxis
            {
                Title = "X Axis (Output Signal)",
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                Position = AxisPosition.Bottom
            });

            LsFilterOut = new LineSeries();

            PlotModelOutputSignal.Series.Add(LsFilterOut);

            #endregion

            // Filter Testing
            double fs = 128;
            double frequency1 = 10;
            double frequency2 = 60;

            int signalLength = 100;

            double[] inputSignal = new double[signalLength];

            for (int i = 0; i < signalLength; i++)
            {
                double inputValue = Math.Sin(2 * Math.PI * frequency1 * i / fs) +
                    (0.2 * Math.Sin(2 * Math.PI * frequency2 * i / fs)); // second part (after the '+') is the noise
                
                inputSignal[i] = inputValue;
            }

            FirFilter fir = new FirFilter();

            double[] outputSignal = fir.FilterSignal(inputSignal);

            for (int i = 0; i < outputSignal.Length;i++)
            {
                LsFilterIn.Points.Add(new DataPoint(i, inputSignal[i]));
                LsFilterOut.Points.Add(new DataPoint(i, outputSignal[i]));
            }

            PlotModelInputSignal.InvalidatePlot(true);
            PlotModelOutputSignal.InvalidatePlot(true);
        }
    }
}
