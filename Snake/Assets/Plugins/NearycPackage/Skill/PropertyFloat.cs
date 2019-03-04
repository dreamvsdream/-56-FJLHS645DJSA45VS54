#region Author & Version
//==================================================== 
// Author：Nearyc 
// File name：......
// Version：V1.0.1
// Date : 2018.10.1
//*Function:
//===================================================
// Fix:
//===================================================

#endregion
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Nearyc.Roslyn;
namespace Nearyc.Skill
{
	[System.Serializable]
	public class PropertyFloat
	{
		[SerializeField] float basee;
		[SerializeField] float min;
		[SerializeField] float _max;
		List<PropertyNode<float>> _addList;
		List<PropertyNode<float>> _moreList;
		uint _index;
		public float Max => _max;
		public float Current => _current.Value;
		public FloatReactiveProperty _current;
		public FloatReactiveProperty CurrentPro => _current;
		public float Percent =>(float)_current.Value / _max;

		public System.Action onCurrentEnterZero;

		public PropertyFloat(float @base = 0, float? current = null, float? min = null)
		{
			this.Init(@base, current, min);
		}

		public void Init(float @base = 0, float? current = null, float? min = null)
		{
			this.basee = @base == 0 ? this.basee : @base;

			if (_addList == null)
			{
				_addList = new List<PropertyNode<float>>();
			}
			else
			{
				_addList.Clear();
			}
			if (_moreList == null)
			{
				_moreList = new List<PropertyNode<float>>();
			}
			else
			{
				_moreList.Clear();
			}
			this._current =this._current?? new FloatReactiveProperty();
			this.min = min.HasValue ? min.Value : 0;
			OnValueChanged();
			this._current.Value = current.HasValue ? current.Value : _max;
		}

		public void ModifyCurrent(float amount)
		{
			_current.Value += amount;
			if (_current.Value > _max)
			{
				_current.Value = _max;
			}
			if (_current.Value < min)
			{
				_current.Value = min;
				if (onCurrentEnterZero != null)
					onCurrentEnterZero();
			}
		}

		private void OnValueChanged()
		{
			if (_max == 0) _max = basee == 0 ? 1 : basee;

			var tempPercent = this.Percent;
			float moreSum = 0;
			_moreList.ForEach(x => moreSum += x.value);
			if (moreSum <= -100)
			{
				_current.Value = _max = 0;
				return;
			}
			float addSum = 0;
			_addList.ForEach(x => addSum += x.value);
			_max = (basee + addSum) * (1 + moreSum / 100);
			_current.Value = (_max * tempPercent);
		}

		public uint Add(PropertyNode<float> change)
		{
			change.Id = ++_index;
			_addList.Add(change);
			OnValueChanged();
			return change.Id;
		}

		public uint Add(float change)
		{
			_addList.Add(new PropertyNode<float>(change,++_index, null));
			OnValueChanged();
			return _index;
		}

		public void RemoveAdd(PropertyNode<float> toRemove)
		{
			_addList.Remove(toRemove);
			OnValueChanged();
		}

		public void RemoveAdd(float change)
		{
			_addList.Remove(new PropertyNode<float>(change, 0, null));
			OnValueChanged();
		}

		public uint More(PropertyNode<float> change)
		{
			change.Id = ++_index;
			_moreList.Add(change);
			OnValueChanged();
			return change.Id;
		}

		public uint More(float change)
		{
			_moreList.Add(new PropertyNode<float>(change, ++_index, null));
			OnValueChanged();
			return _index;
		}

		public void RemoveMore(PropertyNode<float> toRemove)
		{
			_moreList.Remove(toRemove);
			OnValueChanged();
		}

		public void RemoveMore(float change)
		{
			_moreList.Remove(new PropertyNode<float>(change, 0, null));
			OnValueChanged();
		}

	}
}
