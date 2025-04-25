using UnityEngine.SceneManagement;
using YG;

public class ClearProgressButton : ButtonView
{
    protected override void OnButtonClick()
    {
        YG2.SetDefaultSaves();
        SceneManager.LoadScene(1);
    }
}
