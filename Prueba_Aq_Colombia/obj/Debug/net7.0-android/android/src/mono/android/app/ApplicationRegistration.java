package mono.android.app;

public class ApplicationRegistration {

	public static void registerApplications ()
	{
				// Application and Instrumentation ACWs must be registered first.
		mono.android.Runtime.register ("Prueba_Aq_Colombia.MainApplication, Prueba_Aq_Colombia, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", crc643a9cb6e4b5219c22.MainApplication.class, crc643a9cb6e4b5219c22.MainApplication.__md_methods);
		mono.android.Runtime.register ("Microsoft.Maui.MauiApplication, Microsoft.Maui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", crc6488302ad6e9e4df1a.MauiApplication.class, crc6488302ad6e9e4df1a.MauiApplication.__md_methods);
		
	}
}
