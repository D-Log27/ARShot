using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesScript : MonoBehaviour
{
    public bool pingPong        = true;
    public bool checkDaPos      = false;
    public float speed          = 1f;
    public float distance       = 1f;
    public Vector3 maxDistance  = new Vector3(1, 1, 1);

    public Vector3[] linePositions;
    public bool playAnimation   = true;

    public bool checker     = false;

    Vector3 daCurrPos       = Vector3.zero;
    Vector3 daCurrPosTwo    = Vector3.zero;

    float daLength = 0f;
    float daLengthAdd = 0f;
    float tempDistance = 0f;
    int counter = 0;
    int orgPosCount = 0;
    bool stopEndPoint = true;
    bool stopStartPoint = true;
    bool slowRestart = false;

    void Start()
    {
        checkDaPos = true;
        if(GetComponent<LineRenderer>() && GetComponent<LineRenderer>().positionCount > 0)
            daCurrPos = GetComponent<LineRenderer>().GetPosition(0);
    }

    void Update ()
    {
        if(GetComponent<LineRenderer>()  && GetComponent<LineRenderer>().positionCount > 0)
        {
            if(checkDaPos)
            {
                checkDaPos = false;
                counter = 0;
                orgPosCount = GetComponent<LineRenderer>().positionCount;

                if(pingPong)
                {
                    if(GetComponent<LineRenderer>().GetPosition(0) !=
                            GetComponent<LineRenderer>().GetPosition(orgPosCount - 1) )
                    {
                        GetComponent<LineRenderer>().positionCount = orgPosCount * 2;
                        orgPosCount = GetComponent<LineRenderer>().positionCount;
                        int tempyInt = 0;
                        for(int i = orgPosCount - 1; i > (orgPosCount / 2) - 1; i--)
                        {
                            GetComponent<LineRenderer>().SetPosition(i,
                                    GetComponent<LineRenderer>().GetPosition(tempyInt));
                            tempyInt++;
                        }
                    }
                }
                else
                {
                    if(GetComponent<LineRenderer>().GetPosition(0) !=
                            GetComponent<LineRenderer>().GetPosition(1) )
                    {
                        GetComponent<LineRenderer>().positionCount++;
                        orgPosCount = GetComponent<LineRenderer>().positionCount;
                        for(int i = orgPosCount - 1; i > 0; i--)
                        {
                            GetComponent<LineRenderer>().SetPosition(i,
                                    GetComponent<LineRenderer>().GetPosition(i - 1));
                        }
                    }
                }


                linePositions = new Vector3[orgPosCount];

                for(int i = 0; i < linePositions.Length; i++)
                {
                    linePositions[i] = new Vector3(
                        GetComponent<LineRenderer>().GetPosition(i).x * transform.localScale.x,
                        GetComponent<LineRenderer>().GetPosition(i).y * transform.localScale.y,
                        GetComponent<LineRenderer>().GetPosition(i).z * transform.localScale.z);
                    GetComponent<LineRenderer>().SetPosition(i, Vector3.zero);
                }
                GetComponent<LineRenderer>().positionCount = 2;
                daCurrPos = daCurrPosTwo = linePositions[0];
                GetComponent<LineRenderer>().SetPosition(0, linePositions[0]);
                GetComponent<LineRenderer>().SetPosition(1, linePositions[0]);
                tempDistance = distance;
                stopEndPoint = false;
            }
            else if(!checkDaPos && playAnimation)
            {
                if(!stopEndPoint)
                {
                    daCurrPos = Vector3.MoveTowards(daCurrPos, linePositions[counter + 1], (speed / 10));
                    GetComponent<LineRenderer>().SetPosition( GetComponent<LineRenderer>().positionCount - 1, daCurrPos);
                    daLength = Vector3.Distance(daCurrPos, linePositions[counter]) + daLengthAdd;

                    if(daCurrPos == linePositions[counter + 1])
                    {
                        counter++;
                        GetComponent<LineRenderer>().positionCount++;
                        GetComponent<LineRenderer>().SetPosition( GetComponent<LineRenderer>().positionCount - 1, daCurrPos);
                        daCurrPos = linePositions[counter];
                        daLengthAdd = daLengthAdd + Vector3.Distance(linePositions[counter - 1], linePositions[counter]);

                        if((counter + 1) >= linePositions.Length)
                        {
                            counter = 0;
                        }
                    }
                }

                if(GetComponent<LineRenderer>().positionCount > orgPosCount)
                {
                    slowRestart = true;
                    stopEndPoint = true;
                }
                else if(!slowRestart && daLength > distance)
                {
                    stopStartPoint = false;
                }
                else if (slowRestart && GetComponent<LineRenderer>().positionCount == 2)
                {
                    slowRestart = false;
                    stopEndPoint = false;
                }

                if (!slowRestart && tempDistance != distance)
                {
                    stopEndPoint = true;
                    stopStartPoint = true;
                    counter = 0;
                    daLength = 0;
                    daLengthAdd = 0;
                    GetComponent<LineRenderer>().positionCount = 2;
                    daCurrPos = daCurrPosTwo = linePositions[0];
                    tempDistance = distance;
                    stopEndPoint = false;
                }

                if(!stopStartPoint && GetComponent<LineRenderer>().positionCount > 1)
                {
                    if(daCurrPosTwo == GetComponent<LineRenderer>().GetPosition(1))
                    {
                        for(int i = 1; i < GetComponent<LineRenderer>().positionCount; i++)
                        {
                            GetComponent<LineRenderer>().SetPosition(i - 1, GetComponent<LineRenderer>().GetPosition(i));
                        }
                        GetComponent<LineRenderer>().positionCount--;
                    }
                    else
                    {
                        daCurrPosTwo = Vector3.MoveTowards(daCurrPosTwo, GetComponent<LineRenderer>().GetPosition(1), (speed / 10));
                        GetComponent<LineRenderer>().SetPosition(0, daCurrPosTwo);
                    }
                }
            }
        }
    }

}
