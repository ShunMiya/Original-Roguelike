using System.Collections;
using UnityEngine;
using TMPro;

namespace UISystemV2
{
    public class ADVText : MonoBehaviour
    {
        public GameObject textBox;
        public TextMeshProUGUI textMeshPro1;
        public TextMeshProUGUI textMeshPro2;
        public TextMeshProUGUI textMeshPro3;
        public SystemTextV2 systemTextV2;

        private string Type;
        int TextNum = 0;

        public void TypeSet(string st)
        {
            Type = st;
        }

        public IEnumerator TextBoxSet()
        {
            systemTextV2.NonActive();
            textBox.SetActive(true);
            if (!gameObject.activeSelf) yield return null;

            TextNum = 0;
            while (textBox.gameObject.activeSelf)
            {
                ADVStart(Type);

                if (Input.GetButton("Submit")) TextNum++;
                Input.ResetInputAxes();

                yield return null;
            }
        }

        public void NonActive()
        {
            textMeshPro1.text = "";
            textMeshPro2.text = "";
            textMeshPro3.text = "";
            textBox.SetActive(false);
        }

        private void ADVStart(string Type)
        {
            switch (Type)
            {
                case "説明0":

                    switch(TextNum)
                    {
                        case 0:
                            textMeshPro1.text = "始まりの洞窟へようこそ！";
                            break;
                        case 1:
                            textMeshPro2.text = "ここでは基本操作を学ぶための";
                            textMeshPro1.text = "簡単なチュートリアルを行えます";
                            break;
                        case 2:
                            textMeshPro2.text = "表示されるテキストを読みながら";
                            textMeshPro1.text = "先へ進んでいきましょう";
                            break;
                        case 3:
                            textMeshPro2.text = "それではさっそくキャラクターを";
                            textMeshPro1.text = "動かしてみましょう";
                            break;
                        case 4:
                            NonActive();
                            break;
                    }
                    break;

                case "説明1":
                    switch (TextNum)
                    {
                        case 0:
                            textMeshPro1.text = "アイテムを入手しました";
                            break;
                        case 1:
                            textMeshPro2.text = "回復薬は「ＨＰ」";
                            textMeshPro1.text = "木の実は「満腹度」を回復できます";
                            break;
                        case 2:
                            textMeshPro2.text = "";
                            textMeshPro1.text = "ＨＰはターン経過で徐々に回復していきます";
                            break;
                        case 3:
                            textMeshPro2.text = "しかし、満腹度が０になっていると";
                            textMeshPro1.text = "逆に減るようになってしまいます";
                            break;
                        case 4:
                            textMeshPro2.text = "ＨＰがなくなるとゲームオーバーに";
                            textMeshPro1.text = "なってしまうので注意しましょう";
                            break;
                        case 5:
                            NonActive();
                            break;
                    }
                    break;

                case "説明2":
                    switch (TextNum)
                    {
                        case 0:
                            textMeshPro1.text = "ここから先は通路です";
                            break;
                        case 1:
                            textMeshPro2.text = "「X」キーを入力しながら十字キーを";
                            textMeshPro1.text = "押すことで高速で移動することが出来ます";
                            break;
                        case 2:
                            NonActive();
                            break;
                    }
                    break;

                case "説明3":
                    switch (TextNum)
                    {
                        case 0:
                            textMeshPro1.text = "ここは斜め移動で進むのが楽そうです";
                            break;
                        case 1:
                            textMeshPro2.text = "十字キーを二方向入力することで";
                            textMeshPro1.text = "斜め移動することが出来ます";
                            break;
                        case 2:
                            textMeshPro2.text = "また「C」キーを入力中は移動せずに向きを";
                            textMeshPro1.text = "変えれる為こちらを利用してもいいでしょう";
                            break;
                        case 3:
                            NonActive();
                            break;
                    }
                    break;

                case "説明4":
                    switch(TextNum)
                    {
                        case 0:
                            textMeshPro2.text = "斜め移動を利用すれば移動を最適化出来ます";
                            textMeshPro1.text = "使いこなせると様々な場面で役立つでしょう";
                            break;
                        case 1:
                            NonActive();
                            break;
                    }
                    break;
                case "説明5":
                    switch (TextNum)
                    {
                        case 0:
                            textMeshPro1.text = "目の前の台座から次のフロアへ進めます";
                            break;
                        case 1:
                            textMeshPro2.text = "やり残したことがある場合は";
                            textMeshPro1.text = "「まだ留まる」を選びましょう";
                            break;
                        case 2:
                            NonActive();
                            break;
                    }
                    break;
                case "説明6":
                    switch (TextNum)
                    {
                        case 0:
                            textMeshPro1.text = "このフロアでは戦闘について説明します";
                            break;
                        case 1:
                            textMeshPro2.text = "ダンジョン内の敵はプレイヤーを";
                            textMeshPro1.text = "見つけると攻撃を仕掛けてきます";
                            break;
                        case 2:
                            textMeshPro2.text = "プレイヤーは「Z」キーで";
                            textMeshPro1.text = "正面に攻撃することが出来ます";
                            break;
                        case 3:
                            textMeshPro2.text = "こちらからも敵に近づき";
                            textMeshPro1.text = "「Z」キーで攻撃しましょう";
                            break;
                        case 4:
                            NonActive();
                            break;
                    }
                    break;
                case "説明7":
                    switch (TextNum)
                    {
                        case 0:
                            textMeshPro2.text = "敵はプレイヤーを見つけると";
                            textMeshPro1.text = "攻撃する為近づいてきます";
                            break;
                        case 1:
                            textMeshPro3.text = "先制して攻撃されないよう";
                            textMeshPro2.text = "わざとターンを消費する";
                            textMeshPro1.text = "テクニックもあります";
                            break;
                        case 2:
                            textMeshPro3.text = "例えば今の位置なら";
                            textMeshPro2.text = "攻撃を空振ることで敵から";
                            textMeshPro1.text = "攻撃範囲に入ってくれます";
                            break;
                        case 3:
                            textMeshPro3.text = "";
                            textMeshPro2.text = "";
                            textMeshPro1.text = "敵との距離を見極めて戦いましょう";
                            break;
                        case 4:
                            NonActive();
                            break;
                    }
                    break;
                case "説明8":
                    switch (TextNum)
                    {
                        case 0:
                            textMeshPro1.text = "「武器」を手に入れました";
                            break;
                        case 1:
                            textMeshPro2.text = "武器は装備することで";
                            textMeshPro1.text = "攻撃の威力が上がります";
                            break;
                        case 2:
                            textMeshPro2.text = "メニューからアイテムを選び";
                            textMeshPro1.text = "装備しましょう";
                            break;
                        case 3:
                            NonActive();
                            break;
                    }
                    break;
                case "説明9":
                    switch (TextNum)
                    {
                        case 0:
                            textMeshPro2.text = "部屋の中に複数の敵がいる場合";
                            textMeshPro1.text = "部屋の中で戦わない方が無難です";
                            break;
                        case 1:
                            textMeshPro2.text = "通路におびきだし一対ずつ相手取れば";
                            textMeshPro1.text = "被害は最小限に抑えれます";
                            break;
                        case 2:
                            NonActive();
                            break;
                    }
                    break;
                case "説明10":
                    switch (TextNum)
                    {
                        case 0:
                            textMeshPro1.text = "「防具」を手に入れました";
                            break;
                        case 1:
                            textMeshPro2.text = "防具は装備することで";
                            textMeshPro1.text = "受けるダメージを減らすことが出来ます";
                            break;
                        case 2:
                            textMeshPro2.text = "こちらも忘れずにメニューから";
                            textMeshPro1.text = "装備しましょう";
                            break;
                        case 3:
                            NonActive();
                            break;
                    }
                    break;
                case "説明11":
                    switch (TextNum)
                    {
                        case 0:
                            textMeshPro1.text = "目の前に落ちているのは「投擲武器」です";
                            break;
                        case 1:
                            textMeshPro3.text = "投擲武器はアイテムから使うを選択すると";
                            textMeshPro2.text = "スタックを一つ消費しプレイヤーの";
                            textMeshPro1.text = "正面方向に投擲することが出来ます";
                            break;
                        case 2:
                            textMeshPro3.text = "";
                            textMeshPro2.text = "効果はアイテムごとに違うため";
                            textMeshPro1.text = "試しに正面の岩に投げてみましょう";
                            break;
                        case 3:
                            NonActive();
                            break;
                    }
                    break;
                case "説明13":
                    switch (TextNum)
                    {
                        case 0:
                            textMeshPro1.text = "落石の罠を踏みました";
                            break;
                        case 1:
                            textMeshPro2.text = "落石の罠は対策をしていないとＨＰが";
                            textMeshPro1.text = "７割も減ってしまう恐ろしい罠です";
                            break;
                        case 2:
                            textMeshPro3.text = "ダンジョン内には他にも様々な罠が";
                            textMeshPro2.text = "存在する為、特にＨＰは";
                            textMeshPro1.text = "常に余裕を持って行動しましょう";
                            break;
                        case 3:
                            NonActive();
                            break;
                    }
                    break;
                case "説明14":
                    switch (TextNum)
                    {
                        case 0:
                            textMeshPro1.text = "おつかれさまでした";
                            break;
                        case 1:
                            textMeshPro1.text = "以上でチュートリアルは終了です";
                            break;
                        case 2:
                            textMeshPro2.text = "まだまだ説明が不十分ではありますが";
                            textMeshPro1.text = "あとは、実際にプレイして覚えてください";
                            break;
                        case 3:
                            textMeshPro2.text = "";
                            textMeshPro1.text = "それではよき冒険の旅を祈ります";
                            break;
                        case 4:
                            NonActive();
                            break;
                    }
                    break;
            }
            return;
        }
    }
}