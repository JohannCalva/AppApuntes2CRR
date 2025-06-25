using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using TallerGrupalMMVC.Models;

namespace TallerGrupalMMVC.Services
{
    public class RecordatoriosService
    {
        private readonly string _fileName = "recordatorios.json";
        private string _filePath;

        public RecordatoriosService()
        {
            _filePath = Path.Combine(FileSystem.AppDataDirectory, _fileName);
        }

        public async Task<List<Recordatorio>> GetRecordatoriosAsync()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new List<Recordatorio>();
                }

                var json = await File.ReadAllTextAsync(_filePath);
                var recordatorios = JsonConvert.DeserializeObject<List<Recordatorio>>(json);
                return recordatorios ?? new List<Recordatorio>();
            }
            catch (Exception ex)
            {
                // En caso de error, devolver lista vacía
                return new List<Recordatorio>();
            }
        }

        public async Task SaveRecordatoriosAsync(List<Recordatorio> recordatorios)
        {
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(recordatorios, Newtonsoft.Json.Formatting.Indented);
                await File.WriteAllTextAsync(_filePath, json);
            }
            catch (Exception ex)
            {
                // Manejar el error según necesites
                throw new Exception($"Error al guardar recordatorios: {ex.Message}");
            }
        }

        public async Task<bool> AddRecordatorioAsync(Recordatorio recordatorio)
        {
            try
            {
                var recordatorios = await GetRecordatoriosAsync();
                recordatorios.Add(recordatorio);
                await SaveRecordatoriosAsync(recordatorios);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateRecordatorioAsync(Recordatorio recordatorio)
        {
            try
            {
                var recordatorios = await GetRecordatoriosAsync();
                var index = recordatorios.FindIndex(r => r.Id == recordatorio.Id);
                if (index >= 0)
                {
                    recordatorios[index] = recordatorio;
                    await SaveRecordatoriosAsync(recordatorios);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteRecordatorioAsync(string id)
        {
            try
            {
                var recordatorios = await GetRecordatoriosAsync();
                var recordatorio = recordatorios.FirstOrDefault(r => r.Id == id);
                if (recordatorio != null)
                {
                    recordatorios.Remove(recordatorio);
                    await SaveRecordatoriosAsync(recordatorios);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
