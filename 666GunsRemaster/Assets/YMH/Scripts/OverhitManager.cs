using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Character.Player
{
    public class OverhitManager : MonoBehaviour
    {
        private OverHit overhit;

        private float _overhitTime;                     //오버히트 시간
        private bool[] _isOverhit;                       //오버히트 상태

        //오버히트 시작 시 시간 관련 변수
        private float[] _currentOverhitTime;            //현재 오버히트 시간
        private float _overhitTimeLimit = 3.0f;         //오버히트 지속시간
        private float[] _currentOverhitGauge;                  //오버히트 게이지
        private float _overhitGaugeLimit = 100;         //오버히트 게이지 최대값

        //오버히트 증가 관련 변수
        private float _overhitGaugeIncrease = 1;        //오버히트 게이지 증가량

        //오버히트 감소 관련 변수
        private float _overhitGaugeDecrease = 1;            //오버히트 게이지 감소량
        private float[] _currentOverhitGaugeDecreaseStartTime;   //현재 오버히트 게이지 감소 시작까지 남은 시간
        private float _overhitGauseDecreaseStartTime = 5f;  //오버히트 게이지 감소 시작 시간
        private float[] _currentOverhitGaugeDecreaseTime;
        private float _overhitGaugeDecreaseTime = 1f;       //오버히트 게이지 감소 시간
        private bool[] _isDereeaseOverhitGauge;             //오버히트 게이지 감소 여부

        private void Awake()
        {
            overhit = GetComponent<OverHit>();
        }

        private void Update()
        {
            // 오버히트 게이지 감소 시작까지는 남은 시간 측정
            MeasureOverhitGaugeDecreaseStartTime(0);
            MeasureOverhitGaugeDecreaseStartTime(1);

            //오버히트 게이지 감소
            DecreaseOverhitGauge(0);
            DecreaseOverhitGauge(1);
        }

        //오버히트 초기화
        public void OverhitInit(int weaponIndex)
        {

        }

        //오버히트 게이지 증가
        public void IncreaseOverhitGauge(int weaponIndex)
        {
            _currentOverhitGauge[weaponIndex] = Mathf.Min(_currentOverhitGauge[weaponIndex] + _overhitGaugeIncrease, _overhitGaugeLimit);
            UIManager.Instance.UpdateOverhitSlider(weaponIndex, _currentOverhitGauge[weaponIndex], _overhitGaugeLimit);

            if (_currentOverhitGauge[weaponIndex] >= _overhitGaugeLimit)
            {
                _isOverhit[weaponIndex] = true;
                //오버히트 사운드

                _currentOverhitTime[weaponIndex] = _overhitTimeLimit;
                overhit.gameObject.SetActive(_isOverhit[weaponIndex]);
            }
            else
            {
                _isDereeaseOverhitGauge[weaponIndex] = false;
                _currentOverhitGaugeDecreaseStartTime[weaponIndex] = _overhitGauseDecreaseStartTime;
            }
        }

        // 오버히트 게이지 감소 시작까지 남은 시간 측정
        private void MeasureOverhitGaugeDecreaseStartTime(int weaponIndex)
        {
            if (!_isDereeaseOverhitGauge[weaponIndex])
            {
                _currentOverhitGaugeDecreaseStartTime[weaponIndex] -= Time.deltaTime;

                if(_currentOverhitGaugeDecreaseStartTime[weaponIndex] <= 0)
                {
                    _isDereeaseOverhitGauge[weaponIndex] = true;
                    _currentOverhitGaugeDecreaseTime[weaponIndex] = _overhitGaugeDecreaseTime;
                }
            }
        }

        //오버히트 게이지 감소
        private void DecreaseOverhitGauge(int weaponIndex)
        {
            if (_isDereeaseOverhitGauge[weaponIndex] && _currentOverhitGauge[weaponIndex] >= 0)
            {
                _currentOverhitGaugeDecreaseTime[weaponIndex] -= Time.deltaTime;
                if (_currentOverhitGaugeDecreaseTime[weaponIndex] <= 0)
                {
                    _currentOverhitGauge[weaponIndex] = Mathf.Max(_currentOverhitGauge[weaponIndex] - _overhitGaugeDecrease, 0);
                    UIManager.Instance.UpdateOverhitSlider(weaponIndex, _currentOverhitGauge[weaponIndex], _overhitGaugeLimit);
                }
            }
        }
    }
}