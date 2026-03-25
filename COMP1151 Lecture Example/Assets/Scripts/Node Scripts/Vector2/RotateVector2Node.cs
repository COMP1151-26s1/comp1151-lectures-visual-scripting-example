using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// RotateVector2Node - Custom Visual Scritping Node
/// by Malcolm Ryan
///
/// This node rotates a Vector2 by a specified angle (in degrees).
/// 
/// Licensed under Creative Commons License CC0 Universal
/// https://creativecommons.org/publicdomain/zero/1.0/

namespace WordsOnPlay.Nodes {

[UnitTitle("Rotate")]
[UnitShortTitle("Rotate Vector2")]
[UnitCategory("COMP1151/Vector2")]
public class RotateVector2Node : Unit
{
    [DoNotSerialize, PortLabelHidden]
    public ControlInput inputTrigger;

    [DoNotSerialize, PortLabelHidden]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput vectorValue;

    [DoNotSerialize]
    public ValueInput angleValue;

    [DoNotSerialize]
    public ValueOutput resultValue;

    private Vector2 output;

    protected override void Definition()
    {
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            Vector2 vector = flow.GetValue<Vector2>(vectorValue);
            float angle = flow.GetValue<float>(angleValue);

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector2.up);
            output = rotation * vector;
            return outputTrigger;
        });
        outputTrigger = ControlOutput("outputTrigger");

        vectorValue  = ValueInput<Vector2>("vector", Vector2.zero);
        angleValue = ValueInput<float>("angle", 0);
        resultValue = ValueOutput<Vector2>("result", (flow) => output);

        Requirement(vectorValue, inputTrigger);
        Requirement(angleValue, inputTrigger);
        Succession(inputTrigger, outputTrigger);

        Assignment(inputTrigger,resultValue);
    }
}
}