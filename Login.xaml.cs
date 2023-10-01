using System.Collections;
using System.Text;
using TaskFileManager.HttpClientObjects;

namespace TaskFileManager;

public partial class Login : ContentPage
{
	private HttpClientObject httpClientObject = new HttpClientObject();
    private string defaultUrld = "http://141.134.1.48:8080";
	public Login()
	{
		InitializeComponent();

       

		buttonSubmit.Clicked += (s, e) =>
		{
			
            postLoginForm(usernameField.Text,passwordField.Text);
		};
        buttonRegister.Clicked += (s, e) => {
            this.Navigation.PushAsync(new Register());
        };
	}


    

   

    public async void postLoginForm(string username, string password) 
	{



        var data = new Dictionary<string, string>
{
    {"username", username},
    {"password", password}
};

       

        
       
       
        var res = httpClientObject.getHttpClient().PostAsync(defaultUrld+ "/login", form(data)).Result;
        
        var content = await res.Content.ReadAsStringAsync();
        if (content.Equals("ok")) 
        {
            App.Current.MainPage = new MainPage();
        }
        displayAlert(content);
    }

	public void displayAlert(String message) 
	{
        DisplayAlert("Ok", message, "ok");
    }

    public FormUrlEncodedContent form(Dictionary<string, string> data) 
    {
        return new FormUrlEncodedContent(data);
    }
}