using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// RotateTransform2D - Custom Visual Scritping Node
/// by Malcolm Ryan
///
/// This node uses Rigidbody2D to rotate an object by a given angle in degrees (about the Z axis)
/// Note: This node should be called using the OnFixedUdpate event, not the OnUpdateEvent
/// 
/// Licensed under Creative Commons License CC0 Universal
/// https://creativecommons.org/publicdomain/zero/1.0/

namespace WordsOnPlay.Nodes {

[UnitTitle("Rotate")]
[UnitShortTitle("Rotate Rigidbody2D")]
[UnitCategory("COMP1151/Rigidbody2D")]
public class RotateRigidbody2D : Unit
{
    [DoNotSerialize, PortLabelHidden]
    public ControlInput inputTrigger;

    [DoNotSerialize, PortLabelHidden]
    public ControlOutput outputTrigger;

    [DoNotSerialize, PortLabelHidden]
    public ValueInput gameObjectValue;

    [DoNotSerialize]
    public ValueInput angleValue;

    public enum MoveWith { SetRotation, MoveRotation };
    [DoNotSerialize]
    public ValueInput moveWithValue;

    protected override void Definition()
    {
        //The lambda to execute our node action when the inputTrigger port is triggered.
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
           GameObject go = flow.GetValue<GameObject>(gameObjectValue);
            Rigidbody2D rigidbody = go.GetComponent<Rigidbody2D>();
            if (rigidbody == null)
            {
                throw new NullReferenceException("GameObject does not include a Rigidbody2D component");
            }
            float angle = flow.GetValue<float>(angleValue);

            MoveWith moveWith = flow.GetValue<MoveWith>(moveWithValue);
            switch (moveWith)
            {
                case MoveWith.MoveRotation:
                    rigidbody.MoveRotation(rigidbody.rotation + angle);
                    break;

                case MoveWith.SetRotation:
                    rigidbody.rotation = rigidbody.rotation + angle;
                    break;
            }

            return outputTrigger;
        });
        outputTrigger = ControlOutput("outputTrigger");

        gameObjectValue  = ValueInput<GameObject>("gameObject", null).NullMeansSelf();
        angleValue = ValueInput<float>("angle", 0);
        moveWithValue = ValueInput<MoveWith>("move with", MoveWith.MoveRotation);

        Requirement(gameObjectValue, inputTrigger);
        Requirement(angleValue, inputTrigger);
        Succession(inputTrigger, outputTrigger);
    }


}
}