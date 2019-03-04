using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameMain.ExcelData;
using System.Linq;

public class SnakeP : MonoBehaviour
{
	public SnakeV v;
	public SnakeData m;
    public SnakeData[] mm;

	public TextAsset modelText;

	private void Start()
	{
		m = SerializeHelper.ArrFromJson<SnakeData>(modelText.text).FirstOrDefault();
     

        var json = SerializeHelper.ToJson(m);
      

     

		Bind(v, m);
	}

	private void Update()
	{
		BindUpdate(v, m);
	}

	private void Bind(SnakeV v, SnakeData m)
	{
		v.skillOne.onClick.AddListener(() =>
		{
			m.MovementSpeed++;
		});
	}

	private void BindUpdate(SnakeV v, SnakeData m)
	{
		v.healthSlider.value = m.Life / 100;
	}

    private void Test(SnakeV v, SnakeData m)
    {
        
    }

}


public class BlockP : MonoBehaviour,IBlock
{

    BlockData m;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SnakeP p = collision.gameObject.GetComponent<SnakeP>();
        TakeDamage(m, p.m);
    }

   

    public void TakeDamage(BlockData blockData, SnakeData snakeData)
    {
        blockData.Life--;
        m.Life--;
    }
}

public interface IBlock
{
    void TakeDamage(BlockData blockData, SnakeData snakeData);
}

public interface IMotor
{

}

public interface SKillMgr
{
    Dictionary<ESkillType, SkillData> Dict { get; }
    SkillData GetSkill(ESkillType skillType);
}

public enum ESkillType
{
    One,
    Two
}
