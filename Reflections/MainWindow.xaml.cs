using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
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

                var watch = Stopwatch.StartNew();
                for (int i = 0; i < iterationsTime; i++)
                {
                    var testList = new List<int>();
                }
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;


                ListCreateStandardLab.Content = Calculatetime(elapsedMs);
            }
            catch (Exception ex)
            {

            }

        }

        private void ListCreateReflectionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int iterationsTime = int.Parse(IterationsBox.Text);

                Type listType = typeof(List<int>);

                var watch = Stopwatch.StartNew();
                for (int i = 0; i < iterationsTime; i++)
                {
                    var testList = Activator.CreateInstance(listType);
                }
                var elapsedMs = watch.ElapsedMilliseconds;


                ListCreateReflectionLab.Content = Calculatetime(elapsedMs);
            }
            catch (Exception ex)
            {

            }
        }


        private void ListAddStandardBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int iterationsTime = int.Parse(IterationsBox.Text);

                var list = new List<int>();

                var watch = Stopwatch.StartNew();
                for (int i = 0; i < iterationsTime; i++)
                {
                    list.Add(i);
                }
                var elapsedMs = watch.ElapsedMilliseconds;

                ListAddStandardStd.Content = Calculatetime(elapsedMs);
            }
            catch (Exception ex)
            {

            }
        }


        private void ListAddReflectionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
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

                ListAddReflectionStd.Content = Calculatetime(elapsedMs);
            }
            catch (Exception ex)
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
        private string Calculatetime(long elapsedMs)
        {
            return elapsedMs + " m/s";
        }


        #endregion


    }
}
