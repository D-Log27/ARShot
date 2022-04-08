using System.Collections;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    public Vector3 startAngle = Vector3.zero;
    public bool randomizeValues = true;
    public float minRotationSpeed = 1;
    public float maxRotationSpeed = 5;
    public float maxRotateAngle = 360;
    public float maxStopDuration = 0;

    bool useStops = true;
    public bool moveIt = true;
    bool rotateClockwise = false;
    bool isOnWait = false;

    float rotationSpeed = 0;
    float rotateAngleAdded = 0;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        useStops = maxStopDuration > 0 ? true : false;
        startAngle = this.transform.eulerAngles;

        if (randomizeValues)
        {
            rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        }
        else
        {
            rotationSpeed = maxRotationSpeed;
        }
        rotateAngleAdded = this.transform.eulerAngles.z + maxRotateAngle;
        if(rotateAngleAdded > 360)
        {
            rotateAngleAdded = 360;
        }

        moveIt = true;
    }

    void Update()
    {
        if(moveIt)
        {
            if (rotateClockwise)
            {
                float rotateAmount = Time.deltaTime * rotationSpeed;
                this.transform.Rotate(new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, rotationSpeed * -1), rotateAmount);
                if (this.transform.rotation.eulerAngles.z <= startAngle.z || (this.transform.eulerAngles.z - rotateAmount) < startAngle.z)
                {
                    if (randomizeValues)
                    {
                        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
                    }
                    rotateClockwise = false;
                    if(useStops)
                    {
                        moveIt = false;
                        StartCoroutine(Waiter(maxStopDuration));
                    }
                }
            }
            else
            {
                this.transform.Rotate(new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, rotationSpeed), Time.deltaTime * rotationSpeed);
                if (this.transform.rotation.eulerAngles.z > rotateAngleAdded)
                {
                    if (randomizeValues)
                    {
                        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
                    }
                    rotateClockwise = true;
                    if(useStops)
                    {
                        moveIt = false;
                        StartCoroutine(Waiter(maxStopDuration));
                    }
                }
            }
        }
        else if(!isOnWait)
        {
            Waiter(maxStopDuration);
        }
    }

    IEnumerator Waiter(float waitTime)
    {
        isOnWait = true;
        yield return new WaitForSeconds(waitTime);
        moveIt = true;
    }
}