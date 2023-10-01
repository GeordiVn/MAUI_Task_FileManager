using CommunityToolkit.Maui.Views;
using System.Text.Json;
using System.Threading.Tasks;
using TaskFileManager.ClassObjects;
using TaskFileManager.HttpClientObjects;

namespace TaskFileManager;

public partial class MainPage : FlyoutPage
{
    HttpClientObject client = new HttpClientObject();

	public MainPage()
	{
		InitializeComponent();

        buttonNewTask.Clicked += (s,e) => { ButtonNewTask_Clicked(s, e, true, "new",null); };
        this.Loaded += (s, e) => { getTasks(this); };
        

        
    }

    public static async void getTasks(MainPage page) 
    {
        var content = page.client.getAsync("/task/get/all").Result;
        await page.DisplayAlert("", content, "ok");

        if (!content.Equals("empty"))
        {
            IList<ClassObjects.Task> taskObj = JsonSerializer.Deserialize<IList<ClassObjects.Task>>(content);

            page.stackLayoutTasks.Children.Clear();



            foreach (ClassObjects.Task task in taskObj)
            {

                page.stackLayoutTasks.Children.Add(page.newTaskFrame(task));
            }
        }
        
        
        
    }

    public Frame newTaskFrame(ClassObjects.Task task) 
    {

        Frame newTaskFrame = new Frame();
        newTaskFrame.Margin = new Thickness(0, 0, 0, 20);
        TapGestureRecognizer tgr = new TapGestureRecognizer();
        tgr.Tapped += (s, e) => { ButtonNewTask_Clicked(s, e, false, task.id.ToString() , task); };
        newTaskFrame.GestureRecognizers.Add(tgr);

        StackLayout stackLayout = new StackLayout();

        Label taskLabelTitle = new Label();
        Label taskLabelDate = new Label();
        Label taskLabelDescription = new Label();

        taskLabelTitle.Text = task.title;
        taskLabelDate.Text = task.taskDate;
        taskLabelDescription.Text = task.description;

        stackLayout.Children.Add(taskLabelTitle);
        stackLayout.Children.Add(taskLabelDate);
        stackLayout.Children.Add(taskLabelDescription);

        newTaskFrame.Content = stackLayout;

        return newTaskFrame;
    } 

   
   

    private async void ButtonNewTask_Clicked(object sender, EventArgs e , bool isNew , string taskId, ClassObjects.Task task)
    {
       

       // await DisplayAlert("test", this.Window.Width+" "+ this.Window.Height, "ok");
        this.ShowPopup(new PopUpNewTask(this.Window.Width-((this.Window.Width/100)*20), this.Window.Height-((this.Window.Height/100)*20),this, isNew, taskId, task));
    }
}