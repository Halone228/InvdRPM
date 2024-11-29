using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mainscript : MonoBehaviour
{
    int count = 1;
    int steps = 0;
    [SerializeField]
    float cellScale;
    [SerializeField]
    int height = 20;
    [SerializeField]
    GameObject cellObject;
    [SerializeField]
    int start_y = 50;
    bool is_running = false;
    bool restart = false;
    List<GameObject> particles;
   

    private void OnGUI()
    {
        int cnt = -1;
        GUI.backgroundColor = Color.gray;
        GUI.Label(new Rect(10, start_y + height*++cnt, 100, height), "Количество частиц");
        count = (int)GUI.HorizontalSlider(new Rect(10, start_y+height*++cnt, 100, height), count, 1, 1000);
        string str_count = GUI.TextField(new Rect(10, start_y+height*++cnt, 100, height/2), count.ToString());
        count = int.Parse(str_count);
        if (GUI.Button(new Rect(10, start_y + height * ++cnt, 100, height), is_running ? "Остановить" : "Запустить"))
        {
            is_running = !is_running;
        }
        if(GUI.Button(new Rect(10, start_y + height * ++cnt, 100, height), "Перезапустить"))
        {
            restart = true;
        }
        GUI.Label(new Rect(10, start_y + height * ++cnt, 100, height), $"Количество шагов: {steps}");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is creat

    void Start()
    {
        particles = new List<GameObject>(count);
        for(int i = 0; i < count; ++i) 
        {
            var obj = Instantiate(cellObject);
            obj.transform.localScale = new Vector3(cellScale, cellScale, cellScale);
            obj.transform.position = Vector3.zero;
            particles.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            foreach(GameObject obj in particles)
            {
                Destroy(obj);
            }
            particles = new List<GameObject>(count);
            for(int i = 0; i < count; ++i)
            {
                var obj = Instantiate(cellObject);
                obj.transform.localScale = new Vector3(cellScale, cellScale, cellScale);
                obj.transform.position = Vector3.zero;
                particles.Add(obj);
            }
            restart = false;
            steps = 0;
        }
        if (is_running)
        {
            ++steps;
            foreach (GameObject obj in particles)
            {
                float ran_val = Random.Range(0f, 100f);
                if (ran_val < 25f)
                {
                    obj.transform.Translate(new Vector2(cellScale, 0));
                }
                else if (ran_val < 50f)
                {
                    obj.transform.Translate(new Vector2(-cellScale, 0));
                }
                else if (ran_val < 75f)
                {
                    obj.transform.Translate(new Vector2(0, cellScale));
                }
                else
                {
                    obj.transform.Translate(new Vector2(0, -cellScale));
                }
            }
        }
    }
}
