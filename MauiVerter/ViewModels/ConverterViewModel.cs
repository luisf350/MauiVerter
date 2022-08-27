using System.Collections.ObjectModel;
using System.Windows.Input;
using UnitsNet;

namespace MauiVerter.ViewModels
{
    public class ConverterViewModel : BaseViewModel
    {
        public string QuantityName { get; set; }
        public ObservableCollection<string> FromMeasures { get; set; }
        public ObservableCollection<string> ToMeasures { get; set; }
        public string CurrentFromMeasures { get; set; }
        public string CurrenToMeasures { get; set; }
        public double FromValue { get; set; } = 1;
        public double ToValue { get; set; }

        public ICommand ReturnCommand => new Command(() => Convert());

        public ConverterViewModel(string quantityName)
        {
            QuantityName = quantityName;
            FromMeasures = ToMeasures = LoadMeasures();
            CurrentFromMeasures = FromMeasures.FirstOrDefault();
            CurrenToMeasures = ToMeasures.Skip(1).FirstOrDefault();
            Convert();
        }

        private void Convert()
        {
            ToValue = UnitConverter.ConvertByName(FromValue, QuantityName, CurrentFromMeasures, CurrenToMeasures);
        }

        private ObservableCollection<string> LoadMeasures()
        {
            var types = Quantity.Infos
                .FirstOrDefault(x => x.Name == QuantityName).UnitInfos
                .Select(x => x.Name)
                .ToList();

            return new ObservableCollection<string>(types);
        }
    }
}
