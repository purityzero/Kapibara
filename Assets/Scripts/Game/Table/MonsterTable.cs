using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRecord : Record
{
	public int Hp;
	public int Power;
	public int Magic;
	public float Speed;
	public float CoolTime;
	public string Img;
	public string Atlas;
}

public class MonsterTable : Table<MonsterRecord>
{
	public MonsterTable(List<MonsterRecord> listRecord) : base(listRecord)
	{

	}
}
