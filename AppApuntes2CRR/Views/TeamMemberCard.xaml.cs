using TallerGrupalMMVC.Models;

namespace AppApuntes2CRR.Views;

public partial class TeamMemberCard : ContentView
{
    public static readonly BindableProperty BackgroundColorProperty =
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(TeamMemberCard), Colors.Blue);

    public Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    public TeamMemberCard()
    {
        InitializeComponent();
    }
}