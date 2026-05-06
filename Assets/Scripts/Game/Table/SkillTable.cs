using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRecord : Record
{
	public string Name;
	public int Power;
	public float CoolTime;
	public float AnimationSpeed;
	public string Img;
	public string Atlas;
	public String Prefab;
public string ClassName;
}

public class SkillTable : Table<SkillRecord>
{
	public SkillTable(List<SkillRecord> listRecord) : base(listRecord)
	{

	}
}
