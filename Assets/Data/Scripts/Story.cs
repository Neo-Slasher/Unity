using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class StoryList {
    public List<Story> intro;
    public List<Story> clearEnding1;
    public List<Story> clearEnding2;
    public List<Story> clearEnding3;
    public List<Story> clearEnding4;
    public List<Story> clearEnding5;
    public List<Story> clearEnding6;
    public List<Story> clearEnding7;
    public List<Story> clearEnding8;
    public List<Story> badEnding;
}

[Serializable]
public class Story {
    public string story;
}
