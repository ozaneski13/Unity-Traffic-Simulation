using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Clean : MonoBehaviour
{
    private int i;
    private DirectoryInfo di;
    void OnDestroy()
    {
        for (i = 0; i < 4; i++)

        {
            if (i == 0)

                di = new DirectoryInfo(@"C:\Users\User\Desktop\TrafficSimulation2K\Assets\Resources\screenshotsA");

            else if (i == 1)

                di = new DirectoryInfo(@"C:\Users\User\Desktop\TrafficSimulation2K\Assets\Resources\screenshotsB");

           else if (i == 2)

               di = new DirectoryInfo(@"C:\Users\User\Desktop\TrafficSimulation2K\Assets\Resources\outputA");

           else if (i == 3)

               di = new DirectoryInfo(@"C:\Users\User\Desktop\TrafficSimulation2K\Assets\Resources\outputB");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }
    }
}