using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class TargetHandler : MonoBehaviour
{




    public GameObject targetGm;

    public GameObject [,] targets;

    [SerializeField]
    private const int max_active = 5;


    private int last_x;
    private int last_y;
    private int next_dir;

    public int current_active;

    






    // Start is called before the first frame update
    void Start()
    {


        targets = new GameObject[7,3];


        Clear();

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Clear() {

        for (int i = 0; i < 7; i++)
        {


            GameObject group = targetGm.transform.GetChild(i).gameObject;



            for (int j = 0; j < 3; j++)
            {

                targets[i, j] = group.transform.GetChild(j).gameObject;


                targets[i, j].SetActive(false);
            }
        }


        current_active = 0;
    }





    public IEnumerator RandomSpawn() {


        last_x = Random.Range(2, 5);

        last_y = Random.Range(0, 3);

        current_active++;


        next_dir = last_x - 4 > 0 ? 1 : -1;


        targets[last_x, last_y].SetActive(true);

        targets[last_x, last_y].GetComponent<Renderer>().material.color = Color.green;

        while (true) {



            int x =  Random.Range(1, 3);


            int y = (last_y + next_dir) %3;

            y = y < 0 ? 2 : y;

            int new_x = last_x + (x * next_dir);

           

            if (targets[new_x, y].activeInHierarchy) {  
                y = (y++)%3;

                //if (targets[new_x, y].activeInHierarchy) new_x = new_x + next_dir * -1;
            }


            last_y = y;
            last_x = new_x;


            if (last_x < 2) next_dir = 1;
            else if (last_x > 4) next_dir = -1;
            else next_dir = Random.Range(0, 2) == 0 ? -1 : 1;

            if (targets[new_x, y].activeInHierarchy) continue;
            
            yield return new WaitForSeconds(1);
            while (current_active >= max_active) yield return new WaitForSeconds(0.2f); ;

            targets[new_x, y].SetActive(true);

            targets[new_x, y].GetComponent<Renderer>().material.color = Color.green;

            current_active++;

        }
    }













    IEnumerator Pattern() {


        foreach(GameObject target in targets) { 

            target.SetActive(!target.activeInHierarchy);

                yield return new WaitForSeconds(1.0f);
            }
        
    
        yield return new WaitForSeconds(1.0f); 
    }





}
