using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NEFAB.Repositories;
using NEFAB.Domains;

namespace NEFAB.Views
{
    /// <summary>
    /// Interaction logic for ContainerView.xaml
    /// </summary>
    public partial class ContainerView : UserControl
    {
        private ContainerRepository _repository;

        public ContainerView()
        {
            InitializeComponent();
            _repository = new ContainerRepository();
        }

        private void btnCreateNewContainer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string containerNo = txtCreateContainerNo.Text.Trim();
                string weekYear = txtCreateWeekAndYear.Text.Trim();

                if (string.IsNullOrEmpty(containerNo) || string.IsNullOrEmpty(weekYear))
                {
                    MessageBox.Show("Udfyld venligst både ContainerNo og Uge-År.");
                    return;
                }

                //split weekYear
                string[] parts = weekYear.Split(new char[] { '-', ' ', '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                {
                    MessageBox.Show("Uge-År formatet er forkert. Brug formatet 'Uge-År' (f.eks. '12-2024').");
                    return;
                }
                int week = int.Parse(parts[0]);
                int year = int.Parse(parts[1]);

                Container newContainer = new Container(containerNo)
                {
                    Week = week,
                    Year = year
                };
                _repository.Add(newContainer);
                MessageBox.Show($"Container {containerNo} for uge {week} i år {year} er oprettet.");
                txtCreateContainerNo.Clear();
                txtCreateWeekAndYear.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl: {ex.Message}");
            }
        }
    }
}
