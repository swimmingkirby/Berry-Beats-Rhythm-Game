using UnityEngine;
using UnityEngine.UI;

namespace BerryBeats.Rework
{
    public class ResultScreen : MonoBehaviour
    {
        [SerializeField]
        private Text
            percentHitText,
            normalsText,
            goodsText,
            perfectsText,
            missesText,
            rankText,
            finalScoreText,
            finalComboText;

        public void SetScore(int val)
        {
            finalScoreText.text = val.ToString();
        }
        public void SetRank(char val)
        {
            rankText.text = val.ToString();
        }
        public void SetCombo(int val)
        {
            finalComboText.text = val.ToString();
        }
        public void SetPercent(float val)
        {
            string _val = (val * 100f).ToString();
            percentHitText.text = (_val.Length<=5 ? _val: _val.Substring(0, 5)) + '%';
        }
        public void SetNormal(int val)
        {
            normalsText.text = val.ToString();
        }
        public void SetGood(int val)
        {
            goodsText.text = val.ToString();
        }
        public void SetPerfect(int val)
        {
            perfectsText.text = val.ToString();
        }
        public void SetMissed(int val)
        {
            missesText.text = val.ToString();
        }
    }
}