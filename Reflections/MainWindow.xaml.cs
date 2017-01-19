using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using VendorClassV2;

namespace Reflections
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VendorClass publicSample;
        VendorClass privateSample;
        List<double> StandardCreationList;

        public MainWindow()
        {
            InitializeComponent();

            publicSample = new VendorClass();
            privateSample = new VendorClass();
        }

        #region Speed
        private void ListCreateStandard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int iterationsTime = int.Parse(IterationsBox.Text);
                long elapsedMs = ListCreateStandardTime(iterationsTime);
                ListCreateStandardLabelFill(elapsedMs);
            }
            catch (Exception ex)
            {

            }

        }

        private async void ListCreateReflectionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                ListCreateReflectionBtn.IsEnabled = false;
                long elapsedMs = 0;
                int iterationsTime = int.Parse(IterationsBox.Text);

                elapsedMs = await TestAsync(iterationsTime, elapsedMs);

                ListCrateWithReflectionLabelFill(elapsedMs);
                ListCreateReflectionBtn.IsEnabled = true;

            }
            catch (Exception ex)
            {

            }
        }



        private void ListAddStandardBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long elapsedMs = ListAddStandardTime();
                ListAddStandardLabelFill(elapsedMs);
            }
            catch (Exception ex)
            {

            }
        }

        private void ListAddReflectionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long elapsedMs = ListAddReflectionTime();
                ListAddReflectionLabelFill(elapsedMs);
            }
            catch (Exception ex)
            {

            }

        }

        private void AvgCreateStandardBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int iterationsTime = int.Parse(IterationsBox.Text);
                AvgCreateStandardBtn.IsEnabled = false;

                StandardCreationList = new List<double>();

                Parallel.For(0, 10, i =>
                 {
                     StandardCreationList.Add(ListCreateStandardTime(iterationsTime));
                 });

                long avg = (long)StandardCreationList.Sum(p => p) / (long)StandardCreationList.Count;

                ListCreateStandardLabelFill(avg);
                AvgCreateStandardBtn.IsEnabled = true;

            }
            catch (Exception)
            {

            }
        }


        private void AvgCreateReflectionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int iterationsTime = int.Parse(IterationsBox.Text);
                long elapsedMs = 0;
                StandardCreationList = new List<double>();
                AvgCreateReflectionBtn.IsEnabled = false;

                Task<long> t1 = Task<long>.Factory.StartNew(() => GetElapsedTime(iterationsTime));
                Task<long> t2 = Task<long>.Factory.StartNew(() => GetElapsedTime(iterationsTime));
                Task<long> t3 = Task<long>.Factory.StartNew(() => GetElapsedTime(iterationsTime));
                Task<long> t4 = Task<long>.Factory.StartNew(() => GetElapsedTime(iterationsTime));
                Task<long> t5 = Task<long>.Factory.StartNew(() => GetElapsedTime(iterationsTime));
                Task<long> t6 = Task<long>.Factory.StartNew(() => GetElapsedTime(iterationsTime));
                Task<long> t7 = Task<long>.Factory.StartNew(() => GetElapsedTime(iterationsTime));
                Task<long> t8 = Task<long>.Factory.StartNew(() => GetElapsedTime(iterationsTime));
                Task<long> t9 = Task<long>.Factory.StartNew(() => GetElapsedTime(iterationsTime));
                Task<long> t10 = Task<long>.Factory.StartNew(() => GetElapsedTime(iterationsTime));


                Task.WaitAll(t1,t2,t3,t4,t5,t6,t7,t8,t9,t10);

                StandardCreationList.Add(t1.Result);
                StandardCreationList.Add(t2.Result);
                StandardCreationList.Add(t3.Result);
                StandardCreationList.Add(t4.Result);
                StandardCreationList.Add(t5.Result);
                StandardCreationList.Add(t6.Result);
                StandardCreationList.Add(t7.Result);
                StandardCreationList.Add(t8.Result);
                StandardCreationList.Add(t9.Result);
                StandardCreationList.Add(t10.Result);

                long avg = (long)StandardCreationList.Sum(p => p) / (long)StandardCreationList.Count;

                ListCrateWithReflectionLabelFill(avg);
                AvgCreateReflectionBtn.IsEnabled = true;
            }
            catch (Exception)
            {
            }
        }


        #endregion

        #region Safety
        private void GetVendorPublicItemsBtn_Click(object sender, RoutedEventArgs e)
        {
            VendorPublicItemsListBox.ItemsSource = null;
            VendorPublicItemsListBox.ItemsSource = publicSample.VendorPublicItems;
        }

        private void GetVendorPrivateItemsBtn_Click(object sender, RoutedEventArgs e)
        {
            Type testType = typeof(VendorClass);
            FieldInfo vendorField = testType.GetField("vendorInternalList", BindingFlags.NonPublic | BindingFlags.Instance);

            List<String> vendorPrivate = vendorField.GetValue(privateSample) as List<String>;

            VendorPrivateItemsListBox.ItemsSource = null;
            VendorPrivateItemsListBox.ItemsSource = vendorPrivate;

        }


        #endregion




        #region HelpersArea

        Func<int, long> GetElapsedTime = (iterationsTime) =>
        {
            Type listType = typeof(List<int>);

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < iterationsTime; i++)
            {
                var testList = Activator.CreateInstance(listType);
            }
            var elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;
        };


        private async Task<long> TestAsync(int iterationsTime, long elapsedMs)
        {
            return await Task.Run(() =>
            {

                try
                {
                    Type listType = typeof(List<int>);

                    var watch = Stopwatch.StartNew();
                    for (int i = 0; i < iterationsTime; i++)
                    {
                        var testList = Activator.CreateInstance(listType);
                    }
                    elapsedMs = watch.ElapsedMilliseconds;


                }
                catch (Exception)
                {

                }

                return elapsedMs;

            });

        }


        private string Calculatetime(long elapsedMs)
        {
            return elapsedMs + " m/s";
        }




        private long ListCreateStandardTime(int iterationsTime)
        {
            var watch = Stopwatch.StartNew();
            for (int i = 0; i < iterationsTime; i++)
            {
                var testList = new List<int>();
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;
        }

        private long ListCreateWithReflectionTime(int iterationsTime)
        {

            Type listType = typeof(List<int>);

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < iterationsTime; i++)
            {
                var testList = Activator.CreateInstance(listType);
            }
            var elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;
        }


        private long ListAddStandardTime()
        {
            int iterationsTime = int.Parse(IterationsBox.Text);

            var list = new List<int>();

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < iterationsTime; i++)
            {
                list.Add(i);
            }
            var elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;
        }

        private long ListAddReflectionTime()
        {
            int iterationsTime = int.Parse(IterationsBox.Text);

            var list = new List<int>();

            Type listType = typeof(List<int>);
            Type[] parametersType = { typeof(int) };
            MethodInfo mi = listType.GetMethod("Add", parametersType);

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < iterationsTime; i++)
            {
                mi.Invoke(list, new object[] { i });
            }
            var elapsedMs = watch.ElapsedMilliseconds;
            return elapsedMs;
        }

        private void ListAddStandardLabelFill(long elapsedMs)
        {
            ListAddStandardStd.Content = Calculatetime(elapsedMs);
        }

        private void ListCreateStandardLabelFill(long elapsedMs)
        {
            ListCreateStandardLab.Content = Calculatetime(elapsedMs);
        }

        private void ListCrateWithReflectionLabelFill(long elapsedMs)
        {
            ListCreateReflectionLab.Content = Calculatetime(elapsedMs);
        }

        private void ListAddReflectionLabelFill(long elapsedMs)
        {
            ListAddReflectionStd.Content = Calculatetime(elapsedMs);
        }



        #endregion


    }
}
