using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : Data
{
	public long Gold;
	public long Jam;
	public int Power;
	public int Magic;
	public int HP;
	public int Def;
	public float Speed;
	public float CoolTime;

	public PlayerData()
	{
		Gold = 0;
		Jam = 50;
		Power = 10;
		Magic = 5;
		HP = 100;
		Def = 1;
		Speed = 1.5f;
		CoolTime = 10f;
	}

	public PlayerData(int _gold, long _jam, int _power, int _magic, int _hp, int _def, float _speed, float _CoolTime)
	{
		this.Gold = _gold;
		this.Jam = _jam;
		this.Power = _power;
		this.Magic = _magic;
		this.HP = _hp;
		this.Def = _def;
		this.Speed = _speed;
		this.CoolTime = _CoolTime;
	}
}
