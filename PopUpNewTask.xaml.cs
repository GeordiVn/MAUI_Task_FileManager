using CommunityToolkit.Maui.Views;
using System.Drawing;
using Microsoft.Maui.Devices;
using TaskFileManager.HttpClientObjects;
using TaskFileManager.ClassObjects;

namespace TaskFileManager;

public partial class PopUpNewTask : Popup
{

    private HttpClientObject client = new HttpClientObject();
    public PopUpNewTask(double width, double height,MainPage page,bool isNew,string taskId , ClassObjects.Task task)
    {
        InitializeComponent();

        popup.Size = new Microsoft.Maui.Graphics.Size(width, height);
        buttonSave.Clicked += (s, e) => { saveTask(page,isNew,taskId,task);};

        if(!isNew ) 
        {
            datePicker.Date = DateTime.Parse(task.taskDate);
            entryTitle.Text = task.title;
            editorOmschrijving.Text = task.description;
            labelHeader.Text = "Task Edit";
        }
    }


    public async void saveTask(MainPage page , bool isNew ,string taskId, ClassObjects.Task task)
    {
        Dictionary<string, string> data = new Dictionary<string, string>{
                {"userkey","geordi" },
                {"taskdate", datePicker.Date.ToString("dd/MM/yyyy HH:mm:ss")},
                {"title", entryTitle.Text},
                {"description",editorOmschrijving.Text}
            }; ;

        if (isNew)
        {

            data.Add("taskid", "new");
        }
        else 
        {
            data.Add("taskid", task.id.ToString());
        }
        
            






        var res = client.getHttpClient().PostAsync(client.getDefaultUrl() + "/task/new", new FormUrlEncodedContent(data)).Result;

        var content = await res.Content.ReadAsStringAsync();
        if (content.Equals("ok"))
        {
           Close();
           MainPage.getTasks(page);
        }
    }
}