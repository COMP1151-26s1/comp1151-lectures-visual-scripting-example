using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// RotateTransform2D - Custom Visual Scritping Node
/// by Malcolm Ryan
///
/// This node uses Transform to rotate an object by a given angle in degrees (about the Z axis)
/// 
/// Licensed under Creative Commons License CC0 Universal
/// https://creativecommons.org/publicdomain/zero/1.0/

namespace WordsOnPlay.Nodes 
{

[UnitTitle("Set Rotation")]
[UnitShortTitle("Set Rotation : Transform")]
[UnitCategory("COMP1151/Transform")]
public class SetRotationTransform2DNode : Unit
{
    [DoNotSerialize, PortLabelHidden]
    public ControlInput inputTrigger;

    [DoNotSerialize, PortLabelHidden]
    public ControlOutput outputTrigger;

    [DoNotSerialize, PortLabelHidden]
    public ValueInput transformValue;

    [DoNotSerialize]
    public ValueInput angleValue;

    [DoNotSerialize]
    public ValueInput spaceValue;

    protected override void Definition()
    {
        //The lambda to execute our node action when the inputTrigger port is triggered.
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            //Making the resultValue equal to the input value from myValueA concatenating it with myValueB.
            Transform transform = flow.GetValue<Transform>(transformValue);
            float angle = flow.GetValue<float>(angleValue);
            Space space = flow.GetValue<Space>(spaceValue);
            Quaternion rotation = Quaternion.Euler(angle * Vector3.forward);

            switch (space)
            {
                case Space.World:
                    transform.rotation = rotation;
                    break;

                case Space.Self:
                    transform.localRotation = rotation;
                    break;
            }

            return outputTrigger;
        });
        outputTrigger = ControlOutput("outputTrigger");

        transformValue  = ValueInput<Transform>("transform", null).NullMeansSelf();
        angleValue = ValueInput<float>("angle", 0);
        spaceValue = ValueInput<Space>("space", Space.World);

        Requirement(transformValue, inputTrigger);
        Requirement(angleValue, inputTrigger);
        Requirement(spaceValue, inputTrigger);
        Succession(inputTrigger, outputTrigger);
    }
}
}