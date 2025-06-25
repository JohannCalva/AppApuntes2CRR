using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TallerGrupalMMVC.Models;

namespace TallerGrupalMMVC.ViewModels
{
    public class AboutViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TeamMember> _teamMembers;

        public ObservableCollection<TeamMember> TeamMembers
        {
            get => _teamMembers;
            set
            {
                if (_teamMembers != value)
                {
                    _teamMembers = value;
                    OnPropertyChanged();
                }
            }
        }

        public AboutViewModel()
        {
            LoadTeamMembers();
        }

        private void LoadTeamMembers()
        {
            TeamMembers = new ObservableCollection<TeamMember>
            {
                new TeamMember
                {
                    Name = "Johann Calva",
                    Age = 21,
                    FavoriteSport = "Fútbol",
                    PhotoPath = "https://purina.cl/sites/default/files/2025-04/razas-de-gatos.jpg", 
                    Role = "Desarrolladora Backend",
                    Description = "Apasionado por el desarrollo móvil ,interfaces de usuario y pro player en Fortnite"
                },
                new TeamMember
                {
                    Name = "Mathias Ruales",
                    Age = 22,
                    FavoriteSport = "Taekwando",
                    PhotoPath = "https://i.blogs.es/fed92e/topicochinos/450_1000.webp",
                    Role = "Desarrolladora Backend",
                    Description = "Especialista en APIs y arquitectura de software y profundo fan del Anime y Marvel."
                },
                new TeamMember
                {
                    Name = "Jean Rodriguez",
                    Age = 20,
                    FavoriteSport = "Baloncesto",
                    PhotoPath = "https://i.pinimg.com/736x/95/18/c7/9518c7baecedd451cc171af7ec775a51.jpg",
                    Role = "Desarrollador Frontend",
                    Description = "Creativo enfocado en desarrollo front y apasionado por el Fortnite ."
                },

            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}