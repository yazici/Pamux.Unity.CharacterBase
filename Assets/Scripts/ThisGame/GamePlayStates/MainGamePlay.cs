namespace Pamux.Zodiac
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public enum StartPointGeneration
    {
        None, Ordered, OrderedPingPong, Random
    }

    public enum ItemTypes
    {
        None, Asteroid, Enemy, Extractable, Turret, Satellite, Planet, Background
    }

    public class MainGamePlay : Abstracts.GamePlayState//, ITextAssetHandler
    {
        //public static List<int> levelLengths = new List<int>() { 180, 300, 300, 360, 240, 420, 540, 300, 420, 540, 900, 60 };

        private List<LevelItem> levelItems = new List<LevelItem>();
        private List<GameObject> spawnedItems = new List<GameObject>();
        public float startFrom = -1.0f;
        public int LevelTime { get { return (int)(Time.time - _doRunStarted); } }
        public int UniversalTime { get { return (int)(Time.time - _doRunStarted + (int)startFrom); } }

        protected override void DoBeforeStart()
        {
            UI.GamePlay.INSTANCE.pnlHUD.enabled = true;
            UI.GamePlay.INSTANCE.pnlToolbar.enabled = true;
            UI.GamePlay.INSTANCE.pnlSummary.enabled = false;

            GameController.INSTANCE.spaceBackgroundCamera.SetGamePlayValues();

        }

        internal override IEnumerator DoRun()
        {
            if (Time.timeScale == 0)
            {
                UI.GamePlay.INSTANCE.lblStatus.text = "PAUSED";
            }
            //GamePlay.INSTANCE.lblStatus.text = "LEVEL";



            foreach (var item in levelItems)
            {
                if (item.time < startFrom)
                {
                    continue;
                }

                yield return new WaitForSeconds(item.time - (Time.time - _doRunStarted) - startFrom);

                spawnedItems.Add(item.Spawn());
            }

            _doRunCompleted = Time.time;
        }

        public Targetable GetSeekableTarget(float aboveZ)
        {
            foreach (var item in spawnedItems)
            {
                if (item != null)
                {
                    Targetable t = item.GetComponent<Targetable>();
                    if (t != null && t.targetedBy == null && t.transform.position.z > aboveZ)
                    {
                        return t;
                    }
                }
            }
            return null;
        }

        internal override bool vIsComplete()
        {
            if (spawnedItems.Count == 0)
            {
                return false;
            }

            if (Player.INSTANCE != null)
            {
                foreach (var item in spawnedItems)
                {
                    if (item != null)
                    {
                        return false;
                    }
                }
            }

            _isComplete = true;
            return true;
        }

        //#region ITextAssetHandler
        //public static int s_GroupId = 1;
        //public void AddItems(string[] header, string[] fields)
        //{
        //    LevelItemInfo lii = new LevelItemInfo(s_GroupId++, header, fields);

        //    int direction = -1;
        //    for (int i = 0; i < lii.itemCount; ++i)
        //    {
        //        Add(lii.GetLevelItem(i, ref direction));
        //    }
        //}



        //public void SetVariable(string name, string value)
        //{

        //}
        //public void OnItemsReady()
        //{
        //    levelItems.Sort();
        //}
        //#endregion

        internal void Add(LevelItem li)
        {
            levelItems.Add(li);
        }

        //internal bool Generate(int groupId, int Id)
        //{
        //    int start = 0;
        //    for (int i = 0; i < Id-1; ++i )
        //        start += levelLengths[i];
        //    int end = start + levelLengths[Id-1];
        //    return Generate(groupId, start, end);
        //}

        //internal bool Generate(int start, int end)
        //{
        //    for (int t = start; t <= end; t += 5)
        //    {
        //        LevelItemInfo lii = new LevelItemInfo(groupId, t, ItemTypes.Enemy, "UnityShip");

        //        if (t % 2 == 0)
        //        {
        //            lii.itemPathVariant = PathVariants.None;
        //        }
        //        else
        //        {
        //            lii.itemPathVariant = PathVariants.Horizontal;
        //        }

        //        lii.itemPathName = @"\";
        //        lii.itemCount = 5;
        //        int direction = -1;
        //        for (int i = 0; i < lii.itemCount; ++i)
        //        {
        //            levelItems.Add(lii.GetLevelItem(i, ref direction));

        //        }
        //    }
        //    levelItems.Sort();
        //    return true;
        //}
    }
}