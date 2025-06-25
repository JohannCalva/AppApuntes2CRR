using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TallerGrupalMMVC.Models;
using TallerGrupalMMVC.Services;

namespace TallerGrupalMMVC.ViewModels
{
    public class RecordatoriosViewModel : INotifyPropertyChanged
    {
        private readonly RecordatoriosService _recordatoriosService;
        private ObservableCollection<Recordatorio> _recordatorios;
        private bool _isLoading;
        private string _nuevoTexto = "";
        private DateTime _nuevaFecha = DateTime.Now.AddHours(1);
        private TimeSpan _nuevaHora = DateTime.Now.AddHours(1).TimeOfDay;

        public ObservableCollection<Recordatorio> Recordatorios
        {
            get => _recordatorios;
            set
            {
                _recordatorios = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public string NuevoTexto
        {
            get => _nuevoTexto;
            set
            {
                _nuevoTexto = value;
                OnPropertyChanged();
                ((Command)AgregarRecordatorioCommand).ChangeCanExecute();
            }
        }

        public DateTime NuevaFecha
        {
            get => _nuevaFecha;
            set
            {
                _nuevaFecha = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan NuevaHora
        {
            get => _nuevaHora;
            set
            {
                _nuevaHora = value;
                OnPropertyChanged();
            }
        }

        public ICommand AgregarRecordatorioCommand { get; }
        public ICommand EliminarRecordatorioCommand { get; }
        public ICommand ToggleActivoCommand { get; }
        public ICommand RefreshCommand { get; }

        public RecordatoriosViewModel()
        {
            _recordatoriosService = new RecordatoriosService();
            _recordatorios = new ObservableCollection<Recordatorio>();

            AgregarRecordatorioCommand = new Command(async () => await AgregarRecordatorio(), () => !string.IsNullOrWhiteSpace(NuevoTexto));
            EliminarRecordatorioCommand = new Command<Recordatorio>(async (recordatorio) => await EliminarRecordatorio(recordatorio));
            ToggleActivoCommand = new Command<Recordatorio>(async (recordatorio) => await ToggleActivo(recordatorio));
            RefreshCommand = new Command(async () => await CargarRecordatorios());

            _ = CargarRecordatorios();
        }

        private async Task CargarRecordatorios()
        {
            try
            {
                IsLoading = true;
                var recordatorios = await _recordatoriosService.GetRecordatoriosAsync();

                Recordatorios.Clear();
                foreach (var recordatorio in recordatorios.OrderBy(r => r.FechaHora))
                {
                    Recordatorios.Add(recordatorio);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar recordatorios: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task AgregarRecordatorio()
        {
            try
            {
                var fechaHoraCompleta = NuevaFecha.Date + NuevaHora;

                var nuevoRecordatorio = new Recordatorio
                {
                    Texto = NuevoTexto.Trim(),
                    FechaHora = fechaHoraCompleta,
                    Activo = true
                };

                var exito = await _recordatoriosService.AddRecordatorioAsync(nuevoRecordatorio);

                if (exito)
                {
                    Recordatorios.Add(nuevoRecordatorio);

                    // Limpiar campos
                    NuevoTexto = "";
                    NuevaFecha = DateTime.Now.AddHours(1);
                    NuevaHora = DateTime.Now.AddHours(1).TimeOfDay;

                    // Reordenar lista
                    var recordatoriosOrdenados = Recordatorios.OrderBy(r => r.FechaHora).ToList();
                    Recordatorios.Clear();
                    foreach (var r in recordatoriosOrdenados)
                    {
                        Recordatorios.Add(r);
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo agregar el recordatorio", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al agregar recordatorio: {ex.Message}", "OK");
            }
        }

        private async Task EliminarRecordatorio(Recordatorio recordatorio)
        {
            try
            {
                bool confirmar = await Application.Current.MainPage.DisplayAlert(
                    "Confirmar",
                    $"¿Estás seguro de eliminar el recordatorio: {recordatorio.Texto}?",
                    "Sí", "No");

                if (confirmar)
                {
                    var exito = await _recordatoriosService.DeleteRecordatorioAsync(recordatorio.Id);
                    if (exito)
                    {
                        Recordatorios.Remove(recordatorio);
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar el recordatorio", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al eliminar recordatorio: {ex.Message}", "OK");
            }
        }

        private async Task ToggleActivo(Recordatorio recordatorio)
        {
            try
            {
                recordatorio.Activo = !recordatorio.Activo;
                await _recordatoriosService.UpdateRecordatorioAsync(recordatorio);
                OnPropertyChanged(nameof(Recordatorios));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al actualizar recordatorio: {ex.Message}", "OK");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}