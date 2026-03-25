using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// GetInputAxisNode - Custom Visual Scritping Node
/// by Malcolm Ryan
///
/// This node reads the value of an axis action input.
/// 
/// Licensed under Creative Commons License CC0 Universal
/// https://creativecommons.org/publicdomain/zero/1.0/

namespace WordsOnPlay.Nodes {

[UnitTitle("GetInputAxis")]
[UnitShortTitle("GetInputAxis")]
[UnitCategory("COMP1151/Input")]
public class GetInputAxisNode : Unit
{

    [DoNotSerialize,PortLabelHidden]
    public ControlInput inputTrigger;

    [DoNotSerialize,PortLabelHidden]
    public ControlOutput outputTrigger;

    [DoNotSerialize]
    public ValueInput inputValue;

    [DoNotSerialize]
    public ValueInput mappingValue;

    [DoNotSerialize]
    public ValueInput actionValue;

    [DoNotSerialize]
    public ValueOutput resultValue;

    private float output;

    protected override void Definition()
    {
        inputTrigger = ControlInput("inputTrigger", (flow) =>
        {
            InputActionAsset input = flow.GetValue<InputActionAsset>(inputValue);
            InputActionMap mapping = input.FindActionMap(flow.GetValue<string>(mappingValue));
            if (mapping == null) 
            {
                throw new ArgumentException($"{input.name} does not include the mapping '{flow.GetValue<string>(mappingValue)}'");
            }

            InputAction action = mapping.FindAction(flow.GetValue<string>(actionValue));
            if (action == null) 
            {
                throw new ArgumentException($"{input.name}.{mapping.name} does not include the action '{flow.GetValue<string>(actionValue)}'");
            }

            output = action.ReadValue<float>();
            return outputTrigger;
        });
        outputTrigger = ControlOutput("outputTrigger");

        inputValue = ValueInput<InputActionAsset>("input asset", null);
        mappingValue = ValueInput<string>("mapping", String.Empty);
        actionValue = ValueInput<string>("action", String.Empty);
        resultValue = ValueOutput<float>("result", (flow) => output);

        Requirement(inputValue, inputTrigger);
        Requirement(mappingValue, inputTrigger);
        Requirement(actionValue, inputTrigger);
        Succession(inputTrigger, outputTrigger);

        Assignment(inputTrigger,resultValue);
    }
}

}