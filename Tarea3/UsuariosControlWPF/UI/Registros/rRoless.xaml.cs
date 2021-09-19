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
using System.Windows.Shapes;
using UsuariosControlWPF.BLL;
using UsuariosControlWPF.Entidades;

namespace UsuariosControlWPF.UI.Registros
{
    /// <summary>
    /// Interaction logic for rRoless.xaml
    /// </summary>
    public partial class rRoless : Window
    {
        public class DateFormat : System.Windows.Data.IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value == null) return null;

                return ((DateTime)value).ToString("dd/MM/yyyy");
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
        private Roles rol = new Roles();
        public rRoless()
        {
            InitializeComponent();
            this.DataContext = rol;
        }

        private void Limpiar()
        {
            this.rol = new Roles();
            this.DataContext = rol;
        }

        private bool Validar()
        {
            bool esValido = true;

            if (FechaDatePicker.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Ha ocurrido un error, inserte la fecha", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (DescripcionTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Ha ocurrido un error, inserte la descripcion", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return esValido;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var roless = RolesBLL.Buscar(Utilidades.ToInt(RolesIdTextBox.Text));
            if (roless != null)
                this.rol = roless;
            else
            {
                this.rol = new Roles();
                MessageBox.Show("No se ha Encontrado", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            this.DataContext = this.rol;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if (RolesBLL.ExisteDescripcion(DescripcionTextBox.Text))
            {
                MessageBox.Show("Ya existe este rol. Ingrese otro", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var paso = RolesBLL.Guardar(this.rol);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Transaccion exitosa!", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Transaccion Fallida", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (RolesBLL.Eliminar(Utilidades.ToInt(RolesIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro eliminado!", "Exito",
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No fue posible eliminar", "Fallo",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
