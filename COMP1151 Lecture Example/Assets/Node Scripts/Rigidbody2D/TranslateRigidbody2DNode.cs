using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// RotateTransform2D - Custom Visual Scritping Node
/// by Malcolm Ryan
///
/// This node uses Rigidbody2D to translate an object by a given vector
///
/// Note: This node should be called using the OnFixedUdpate event, not the OnUpdateEvent
/// 
/// Licensed under Creative Commons License CC0 Universal
/// https://creativecommons.org/publicdomain/zero/1.0/

namespace WordsOnPlay.Nodes {

[UnitTitle("Translate")]
[UnitShortTitle("Translate Rigidbody2D")]
[UnitCategory("COMP1151/Rigidbody2D")]
public class TranslateRigidbody2D : Unit
{
    [DoNotSerialize, PortLabelHidden]
    public ControlInput inputTrigger;

    [DoNotSerialize, PortLabelHidden]
    public ControlOutput outputTrigger;

    [DoNotSerialize, PortLabelHidden]
    public ValueInput gameObjectValue;

    [DoNotSerialize]
    public ValueInput vectorValue;

    [DoNotSerialize]
    public ValueInput spaceValue;

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
            Vector2 v = flow.GetValue<Vector2>(vectorValue);
            Space space = flow.GetValue<Space>(spaceValue);

            if (space == Space.Self)
            {
                // transform the vector into world space
                v = go.transform.TransformVector(v);
            }

            rigidbody.MovePosition(rigidbody.position + v);
            return outputTrigger;
        });
        outputTrigger = ControlOutput("outputTrigger");

        gameObjectValue  = ValueInput<GameObject>("gameObject", null).NullMeansSelf();
        vectorValue = ValueInput<Vector2>("vector", Vector2.zero);
        spaceValue = ValueInput<Space>("space", Space.World);

        Requirement(gameObjectValue, inputTrigger);
        Requirement(vectorValue, inputTrigger);
        Succession(inputTrigger, outputTrigger);
    }


}
}