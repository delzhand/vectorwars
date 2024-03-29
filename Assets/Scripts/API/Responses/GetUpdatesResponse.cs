﻿using System;
using System.Collections.Generic;

[Serializable]
public class VersionUpdate
{
    public string id;
    public string label;
    public string path;
    public int size;
}

[Serializable]
public class VersionUpdateList
{
    public List<VersionUpdate> updates;
}