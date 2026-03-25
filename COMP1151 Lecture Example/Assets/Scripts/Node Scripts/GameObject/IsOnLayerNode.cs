using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// IsOnLayerNode - Custom Visual Scritping Node
/// by Malcolm Ryan
///
/// This node detects whether a GameObject is on one of the layers in the specified LayerMask.
/// 
/// Licensed under Creative Commons License CC0 Universal
/// https://creativecommons.org/publicdomain/zero/1.0/

namespace WordsOnPlay.Nodes {

[UnitTitle("IsOnLayer")]
[UnitShortTitle("IsOnLayer")]
[UnitCategory("COMP1151/GameObject")]
public class IsOnLayerNode : Unit
{
    [DoNotSerialize, PortLabelHidden]
    public ControlInput inputTrigger;

    [DoNotSerialize, PortLabelHidden]
    public ControlOutput outputTrigger;

    [DoNotSerialize, PortLabelHidden]
    public ValueInput gameObjectValue;

    [DoNotSerialize]
    public ValueInput layerMaskValue;

    [DoNotSerialize]
    public ValueOutput resultValue;

    private bool output;

    protected override void Definition()
    {
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            GameObject obj = flow.GetValue<GameObject>(gameObjectValue);
            LayerMask layerMask = flow.GetValue<LayerMask>(layerMaskValue);

            output = (layerMask.value & (1 << obj.layer)) != 0;
            return outputTrigger;
        });
        outputTrigger = ControlOutput("outputTrigger");

        gameObjectValue  = ValueInput<GameObject>("object").NullMeansSelf();
        layerMaskValue  = ValueInput<LayerMask>("layer mask");
        resultValue = ValueOutput<bool>("result", (flow) => output);

        Requirement(gameObjectValue, inputTrigger);
        Requirement(layerMaskValue, inputTrigger);
        Succession(inputTrigger, outputTrigger);

        Assignment(inputTrigger,resultValue);
    }
}

}