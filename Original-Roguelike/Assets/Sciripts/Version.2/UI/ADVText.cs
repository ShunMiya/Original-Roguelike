using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ADVSystem;

namespace UISystemV2
{
    public class ADVText : MonoBehaviour
    {
        public GameObject textBox;
        public TextMeshProUGUI textMeshPro;
        public SystemTextV2 systemTextV2;
        [SerializeField] private GameObject ADVTriggers;

        private string Type;
        private Dictionary<string, List<string>> textDatabase;

        public void Start()
        {
            InitializeTextDatabase();
        }

        private void InitializeTextDatabase()
        {
            textDatabase = new Dictionary<string, List<string>>();

            textDatabase["説明0"] = new List<string>
            {
                "始まりの洞窟へようこそ！",
                "ここでは基本操作を学ぶための簡単な\nチュートリアルを行えます",
                "表示されるテキストを読みながら\n先へ進んでいきましょう",
                "それではさっそくキャラクターを\n動かしてみましょう"
            };

            textDatabase["説明1"] = new List<string>
            {
                "アイテムを入手しました",
                "回復薬は「ＨＰ」\n木の実は「満腹度」を回復できます"
            };

            textDatabase["説明1.1"] = new List<string>
            {
                "ＨＰはターン経過で徐々に回復していきます",
                "しかし、満腹度が０になっていると\n逆に減るようになってしまいます",
                "ＨＰがなくなるとゲームオーバーに\nなってしまうので注意しましょう",
                "また、このダンジョンのアイテムは\n持ち帰れないので使ってみることを\nお勧めします"
            };

            textDatabase["説明2"] = new List<string>
            {
                "ここから先は通路です",
                "ダッシュキーを入力しながら\n移動キーを押すことで高速で移動することが\n出来ます"
            };

            textDatabase["説明3"] = new List<string>
            {
                "ここは斜め移動で進むのが楽そうです",
                "移動キーを二方向入力することで\n斜め移動することが出来ます",
                "また待機キーを入力中は\n移動せずに向きを変えれる為\nこちらを利用してもいいでしょう"
            };

            textDatabase["説明4"] = new List<string>
            {
                "斜め移動を利用すれば移動を最適化出来ます\n使いこなせると様々な場面で役立つでしょう"
            };

            textDatabase["説明5"] = new List<string>
            {
                "目の前の台座から次のフロアへ進めます",
                "やり残したことがある場合は「まだ留まる」\nを選びましょう"
            };

            textDatabase["説明6"] = new List<string>
            {
                "このフロアでは戦闘について説明します",
                "ダンジョン内の敵はプレイヤーを見つけると\n攻撃を仕掛けてきます",
                "プレイヤーは攻撃キーで\n正面に攻撃することが出来ます",
                "こちらからも敵に近づき攻撃キーで\n攻撃しましょう"
            };

            textDatabase["説明7"] = new List<string>
            {
                "敵はプレイヤーを見つけると攻撃する為\n近づいてきます",
                "先制して攻撃されないようわざと\nターンを消費するテクニックもあります",
                "例えば今の位置なら攻撃を空振ることで\n敵から攻撃範囲に入ってくれます",
                "敵との距離を見極めて戦いましょう"
            };

            textDatabase["説明8"] = new List<string>
            {
                "「武器」を手に入れました",
                "武器は装備することで攻撃の威力が\n上がります",
                "メニューからアイテムを選び装備しましょう"
            };

            textDatabase["説明9"] = new List<string>
            {
                "部屋の中に複数の敵がいる場合\n部屋の中で戦わない方が無難です",
                "通路におびきだし一対ずつ相手取れば\n被害は最小限に抑えれます"
            };

            textDatabase["説明10"] = new List<string>
            {
                "「防具」を手に入れました",
                "防具は装備することで受けるダメージを\n減らすことが出来ます",
                "こちらも忘れずにメニューから\n装備しましょう"
            };

            textDatabase["説明11"] = new List<string>
            {
                "目の前に落ちているのは「投擲武器」です",
                "投擲武器はアイテムから使うを選択すると\nスタックを一つ消費しプレイヤーの正面方向\nに投擲することが出来ます",
                "効果はアイテムごとに違うため\n試しに正面の岩に投げてみましょう"
            };

            textDatabase["説明12"] = new List<string>
            {
                "落石の罠を踏みました",
                "落石の罠は対策をしていないとＨＰが７割も\n減ってしまう恐ろしい罠です",
                "ダンジョン内には様々な罠が存在する為\n特にＨＰは常に余裕を持って行動しましょう"
            };

            textDatabase["説明13"] = new List<string>
            {
                "おつかれさまでした",
                "以上でチュートリアルは終了です",
                "まだまだ説明が不十分ではありますが\nあとは、実際にプレイして覚えてください",
                "それではよき冒険の旅を祈ります"
            };

            textDatabase["レベルアップ説明"] = new List<string>
            {
                "レベルアップしました",
                "レベルが上がることでプレイヤーの\nＨＰや攻撃力が上がっていきます"            
            };
        }

        public void TypeSet(string st)
        {
            Type = st;
        }

        public IEnumerator TextBoxSet()
        {
            yield return new WaitForSeconds(0.2f);

            systemTextV2.NonActive();
            textBox.SetActive(true);
            if (!gameObject.activeSelf) yield return null;

            int TextNum = 0;
            while (textBox.gameObject.activeSelf)
            {
                if (textDatabase.ContainsKey(Type))
                {
                    var texts = textDatabase[Type];
                    for (int i = 0; i < texts.Count; i++)
                    {
                        textMeshPro.text = texts[i];

                        yield return new WaitUntil(() => Input.GetButton("Submit"));
                        Input.ResetInputAxes();

                        TextNum++;
                    }

                    NonActive();
                    TextVerUp(Type);
                }

                yield return null;
            }
        }

        public void NonActive()
        {
            textMeshPro.text = "";
            textBox.SetActive(false);
        }

        public void TextVerUp(string Type)
        {
            if(Type == "説明1")
            {
                foreach (Transform child in ADVTriggers.transform)
                {
                    var childScript = child.GetComponent<ADVEvent>();
                    if (childScript != null && childScript.Type == "説明1")
                    {
                        childScript.Type = "説明1.1";
                    }
                }
            }
        }
    }
}