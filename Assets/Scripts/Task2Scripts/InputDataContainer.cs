using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDataContainer
{
    public IDrawingModel drawingModel;
    public static InputDataContainer instance=null;
    public InputDataContainer()
    {
        instance = this;
        drawingModel = new DrawingModel();
    }
}