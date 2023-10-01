
using TaskFileManager.HttpClientObjects;
namespace TaskFileManager;

public partial class Register : ContentPage
{

    private HttpClientObject httpClientObject = new HttpClientObject();
    private string defaultUrld = "http://141.134.1.48:8080";
    public Register()
	{
		InitializeComponent();
        buttonRegister.Clicked += (s, e) => {
            register(usernameRegisterField.Text,passwordRegisterField.Text,emailRegisterField.Text);
        };
	}

    public async void register(string username, string password, string email)
    {

        var data = new Dictionary<string, string>
{
    {"username", username},
    {"email", email},
    {"password", password}
};
      var res =  httpClientObject.getHttpClient().PostAsync(defaultUrld+"/register", form(data)).Result;
        var content = await res.Content.ReadAsStringAsync();
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