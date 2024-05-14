
namespace Prueba_Aq_Colombia.Utilidades
{
    public static class ConexionDB
    {
        public static string DevolderRuta(string NombreDB)
        {
            string rutaDatos = string.Empty;
            if (DeviceInfo.Platform ==DevicePlatform.Android)
            {
                rutaDatos = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                rutaDatos = Path.Combine(rutaDatos, NombreDB);
            }else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                rutaDatos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                rutaDatos = Path.Combine(rutaDatos, "..","Library", NombreDB);
            }
            return rutaDatos;
        }

    }
}
