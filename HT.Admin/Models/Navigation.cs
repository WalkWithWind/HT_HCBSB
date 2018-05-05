using System;
using System.Collections.Generic;
using System.Linq;
using HT.Model;

namespace HT.Admin.Models
{
    public class Navigation
    {
        private readonly Entities _db = new Entities();

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        public List<Menu> GetList(int parentid)
        {
            List<ht_navigation> list = _db.ht_navigation.Where(x => x.id> 0).ToList();
            List<Menu> menus = new List<Menu>();
            GetChilds(list, ref menus, parentid, 0);
            return menus;
        }

        /// <summary>
        /// 使用递归获取子菜单
        /// </summary>
        public void GetChilds(List<ht_navigation> list, ref List<Menu> menus, int parentid, int classlayer)
        {
            classlayer++;
            List<ht_navigation> old = list.Where(x => x.parentid == parentid).OrderBy(x => x.sortid).ThenBy(x => x.id).ToList();
            foreach (ht_navigation item in list)
            {
                if (item.parentid == parentid)
                {
                    Menu menu = new Menu
                    {
                        id = item.id,
                        parentid = Convert.ToInt32(item.parentid),
                        classlayer = classlayer,
                        name = item.name,
                        title = item.title,
                        subtitle = item.subtitle,
                        iconurl = item.iconurl,
                        linkurl = item.linkurl,
                        remark = item.remark,
                        sortid = Convert.ToInt32(item.sortid),
                        actiontype = item.actiontype,
                        isshow = Convert.ToInt32(item.isshow)
                    };
                    menus.Add(menu);
                    GetChilds(list, ref menus, item.id, classlayer);
                }
            }
        }
    }
}