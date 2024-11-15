using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System.IO;

// Author: Charli @CharliSIO
// Observes for variables changes in the INK stories 
public class DialogueObserver
{
    public GameManager m_GameManager;
    public InkManager m_InkManager;
    public Dictionary<string, Ink.Runtime.Object> variables { get; private set;}

    public DialogueObserver(TextAsset loadGlobalsJSON)
    {
        // create the story
        Story globalVariablesStory = new Story(loadGlobalsJSON.text);

        // initialize the dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState)
        {
            Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
        }
    }

    // start listening for variable changes in the story
    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;
    }

    // if a variable has changed update it in the global file
    private void VariableChanged(string variableName, Ink.Runtime.Object value)
    {
        Debug.Log("Variable changed: " + variableName + " = " + value);
        // only maintain variables that were initialised from the global file
        variables.Remove(variableName);
        variables.Add(variableName, value); // remove and readd the variable to update it

        if(variableName == "BossSuspicionCounter")
        {
            m_GameManager.m_iSuspicion = (int)m_InkManager.GetStory().variablesState["BossSuspicionCounter"];
        }
        if(variableName == "HelpCounter")
        {
            m_GameManager.m_iHelperCounter = (int)m_InkManager.GetStory().variablesState["HelpCounter"];
        }
    }

    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}
