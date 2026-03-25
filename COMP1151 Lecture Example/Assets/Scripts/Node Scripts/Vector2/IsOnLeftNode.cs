using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// IsOnLeftNode - Custom Visual Scritping Node
/// by Malcolm Ryan
///
/// This node tests whether one vector is to the left of another.
/// 
/// Licensed under Creative Commons License CC0 Universal
/// https://creativecommons.org/publicdomain/zero/1.0/

namespace WordsOnPlay.Nodes {

[UnitTitle("Is On Left")]
[UnitShortTitle("Is On Left")]
[UnitCategory("COMP1151/Vector2")]
public class IsOnLeftNode : Unit
{
    [DoNotSerialize,PortLabelHidden]
    public ControlInput inputTrigger;

    [DoNotSerialize,PortLabelHidden]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput vector1Value;

    [DoNotSerialize]
    public ValueInput vector2Value;

    [DoNotSerialize]
    public ValueOutput resultValue;

    private bool output;

    protected override void Definition()
    {
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            Vector2 a = flow.GetValue<Vector2>(vector1Value);
            Vector2 b = flow.GetValue<Vector2>(vector2Value);

            output = a.x * b.y < a.y * b.x;
            return outputTrigger;
        });
        outputTrigger = ControlOutput("outputTrigger");

        vector1Value  = ValueInput<Vector2>("v1", Vector2.zero);
        vector2Value  = ValueInput<Vector2>("v2", Vector2.zero);
        resultValue = ValueOutput<bool>("result", (flow) => output);

        Requirement(vector1Value, inputTrigger);
        Requirement(vector2Value, inputTrigger);
        Succession(inputTrigger, outputTrigger);

        Assignment(inputTrigger,resultValue);
    }
}

}