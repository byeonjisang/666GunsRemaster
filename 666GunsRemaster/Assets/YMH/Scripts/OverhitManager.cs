using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Character.Player
{
    public class OverhitManager : MonoBehaviour
    {
        private OverHit overhit;

        public bool[] _isOverhit = new bool[2] { false, false };   //오버히트 상태

        //오버히트 시작 시 시간 관련 변수
        private float[] _currentOverhitTime = new float[2] { 0, 0 };            //현재 오버히트 시간
        private float _overhitTimeLimit;                                        //오버히트 지속시간
        private float[] _currentOverhitGauge = new float[2] { 0, 0 };           //오버히트 게이지
        private float _overhitGaugeLimit;                                 //오버히트 게이지 최대값

        //오버히트 증가 관련 변수
        private float _overhitGaugeIncrease = 4;        //오버히트 게이지 증가량

        //오버히트 감소 관련 변수
        private float _overhitGaugeDecrease = 4;            //오버히트 게이지 감소량
        private float[] _currentOverhitGaugeDecreaseStartTime = new float[2] { 0, 0 };   //현재 오버히트 게이지 감소 시작까지 남은 시간
        private float _overhitGauseDecreaseStartTime = 5f;  //오버히트 게이지 감소 시작 시간
        private float[] _currentOverhitGaugeDecreaseTime;   //현재 오버히트 게이지 감소 시간
        private float _overhitGaugeDecreaseTime = 1f;       //오버히트 게이지 감소 시간
        private bool[] _isDereeaseOverhitGauge = new bool[2] { false, false };  //오버히트 게이지 감소 여부


        private void Update()
        {
            // 오버히트 게이지 감소 시작까지는 남은 시간 측정
            MeasureOverhitGaugeDecreaseStartTime(0);
            MeasureOverhitGaugeDecreaseStartTime(1);

            //오버히트 게이지 감소
            DecreaseOverhitGauge(0);
            DecreaseOverhitGauge(1);

            //오버히트 시작 후 시간 측정
            DecreaseOverhitTime(0);
            DecreaseOverhitTime(1);
        }

        //오버히트 초기화
        public void OverhitInit(PlayerData playerData, OverHit overhit)
        {
            _overhitTimeLimit = playerData.overHitTime;
            _overhitGaugeLimit = playerData.overHitCount;
            _currentOverhitGaugeDecreaseTime = new float[2] { _overhitGaugeDecreaseTime, _overhitGaugeDecreaseTime };

            //오버히트 UI 초기화
            UIManager.Instance.UpdateOverhitSlider(0, _currentOverhitGauge[0], _overhitGaugeLimit);
            UIManager.Instance.UpdateOverhitSlider(1, _currentOverhitGauge[1], _overhitGaugeLimit);

            this.overhit = overhit;
            this.overhit.gameObject.SetActive(false);
        }

        public void OverhitReset(int weaponIndex)
        {
            _currentOverhitGauge[weaponIndex] = 0f;
            _isOverhit[weaponIndex] = false;
            _currentOverhitTime[weaponIndex] = 0f;
            _isDereeaseOverhitGauge[weaponIndex] = false;
            _currentOverhitGaugeDecreaseStartTime[weaponIndex] = _overhitGauseDecreaseStartTime;
            _currentOverhitGaugeDecreaseTime[weaponIndex] = _overhitGaugeDecreaseTime;
            UIManager.Instance.UpdateOverhitSlider(weaponIndex, _currentOverhitGauge[weaponIndex], _overhitGaugeLimit);
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
                overhit.gameObject.SetActive(true);
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

                    _currentOverhitGaugeDecreaseTime[weaponIndex] -= _overhitGaugeDecreaseTime;
                }
            }
        }

        private void DecreaseOverhitTime(int weaponIndex)
        {
            if (_isOverhit[weaponIndex])
            {
                _currentOverhitTime[weaponIndex] -= Time.deltaTime;
                if (_currentOverhitTime[weaponIndex] <= 0)
                {
                    _isOverhit[weaponIndex] = false;
                    _currentOverhitTime[weaponIndex] = _overhitTimeLimit;
                    _currentOverhitGauge[weaponIndex] = 0;

                    UIManager.Instance.UpdateOverhitSlider(weaponIndex, _currentOverhitGauge[weaponIndex], _overhitGaugeLimit);
                }
            }
        }
    }
}