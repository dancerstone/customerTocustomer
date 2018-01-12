using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealMvc.API.Sina
{
    /// <summary>
    /// Sina Entity
    /// </summary>
    public class SinaEntity
    {
        #region 用户 SinaWeiBoUser
        /// <summary>
        /// 用户 SinaWeiBoUser
        /// </summary>
        public class SinaWeiBoUser
        {
            /// <summary>
            /// id 	int64 	用户UID
            /// </summary>
            public string id { get; set; }

            /// </summary>
            ///screen_name 	string 	用户昵称
            /// </summary>
            public string screen_name { get; set; }

            /// </summary>
            ///name 	string 	友好显示名称
            /// </summary>
            public string name { get; set; }

            /// </summary>
            ///province 	int 	用户所在地区ID
            /// </summary>
            public int province { get; set; }

            /// </summary>
            ///city 	int 	用户所在城市ID
            /// </summary>
            public int city { get; set; }

            ///  </summary>
            ///location 	string 	用户所在地
            /// </summary>
            public string location { get; set; }

            ///  </summary>
            ///description 	string 	用户描述
            /// </summary>
            public string description { get; set; }

            ///  </summary>
            ///url 	string 	用户博客地址
            /// </summary>
            public string url { get; set; }

            ///  </summary>
            ///profile_image_url 	string 	用户头像地址
            /// </summary>
            public string profile_image_url { get; set; }

            ///  </summary>
            ///domain 	string 	用户的个性化域名
            /// </summary>
            public string domain { get; set; }

            ///  </summary>
            ///gender 	string 	性别，m：男、f：女、n：未知
            /// </summary>
            public string gender { get; set; }

            ///  </summary>
            ///followers_count 	int 	粉丝数
            /// </summary>
            public int followers_count { get; set; }

            ///  </summary>
            ///friends_count 	int 	关注数
            /// </summary>
            public int friends_count { get; set; }

            ///  </summary>
            ///statuses_count 	int 	微博数
            /// </summary>
            public int statuses_count { get; set; }

            ///  </summary>
            ///favourites_count 	int 	收藏数
            /// </summary>
            public int favourites_count { get; set; }

            ///  </summary>
            ///created_at 	string 	创建时间
            /// </summary>
            public string created_at { get; set; }

            ///  </summary>
            ///following 	boolean 	当前登录用户是否已关注该用户
            /// </summary>
            public bool following { get; set; }

            ///  </summary>
            ///verified 	boolean 	是否是微博认证用户，即带V用户
            /// </summary>
            public bool verified { get; set; }

            ///  </summary>
            ///allow_all_act_msg 	boolean 	是否允许所有人给我发私信
            /// </summary>
            public bool allow_all_act_msg { get; set; }

            ///  </summary>
            ///geo_enabled 	boolean 	是否允许带有地理信息
            /// </summary>
            public bool geo_enabled { get; set; }

        }
        #endregion

        #region 帖子 SinaWeiBoStatus
        /// <summary>
        /// 帖子 SinaWeiBoStatus
        /// </summary>
        public class SinaWeiBoStatus
        {

        }
        #endregion

        #region 评论 SinaWeiBoComment
        /// <summary>
        /// 评论 SinaWeiBoComment
        /// </summary>
        public class SinaWeiBoComment
        {

        } 
        #endregion
    }
}
