using UnityEngine;
using System.Xml.Linq;
using PlayerV2;

namespace Field
{
    public class LoadFieldMap : MonoBehaviour
    {
        public string mapName;
        public Areamap field;

        // �ȉ��̃p�����[�^�[��錾
        public PlayerMoveV2 player;

        // Start is called before the first frame update
        void Start()
        {
            field.Reset();
            Array2D mapdata = readMapFile(mapName);
            if (mapdata != null)
            {
                field.Create(mapdata);
            }
        }

        /**
        * TMX�t�@�C������}�b�v�f�[�^���擾����
        */
        private Array2D readMapFile(string path)
        {
            try
            {
                XDocument xml = XDocument.Load(path);
                XElement map = xml.Element("map");
                Array2D data = null;
                int w = 0, h = 0;
                foreach (var layer in map.Elements("layer"))
                {
                    switch (layer.Attribute("id").Value)
                    {
                        case "1":
                            string[] sdata = (layer.Element("data").Value).Split(',');
                            w = int.Parse(layer.Attribute("width").Value);
                            h = int.Parse(layer.Attribute("height").Value);
                            data = new Array2D(w, h);
                            for (int z = 0; z < h; z++)
                            {
                                for (int x = 0; x < w; x++)
                                {
                                    data.Set(x, z, int.Parse(sdata[x + ToMirrorZ(z, h) * w]) - 1);
                                    //data.Set(x, z, int.Parse(sdata[ToMirrorX(x, w) + z * w]) - 1);
                                }
                            }
                            break;
                    }
                }
                foreach (var objgp in map.Elements("objectgroup"))
                {
                    switch (objgp.Attribute("id").Value)
                    {
                        case "3":
                            foreach (var obj in objgp.Elements("object"))
                            {
                                switch (obj.Attribute("name").Value)
                                {
                                    case "Player":
                                        int x = int.Parse(obj.Attribute("x").Value);
                                        int z = int.Parse(obj.Attribute("y").Value);
                                        int pw = int.Parse(obj.Attribute("width").Value);
                                        int ph = int.Parse(obj.Attribute("height").Value);

                                        player.SetPosition(x / pw, ToMirrorZ(z / ph, h));
                                        break;
                                }
                                break;
                            }
                            break;
                    }
                }

                return data;
            }
            catch (System.Exception i_exception)
            {
                Debug.LogErrorFormat("{0}", i_exception);
            }
            return null;
        }

        /**
         * * Z���ɑ΂��Ĕ��΂̒l��Ԃ�
         */
        private int ToMirrorX(int xgrid, int mapWidth)
        {
            return mapWidth - xgrid - 1;
        }

        /**
         * * X���ɑ΂��Ĕ��΂̒l��Ԃ��B
         */
        private int ToMirrorZ(int zgrid, int mapHeight)
        {
            return mapHeight - zgrid - 1;
        }
    }
}