using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Web;
using Umbraco.Web.Composing;
using WebApplication.Models;

namespace WebApplication
{
    public static class UmbracoContextExtentions
    {
        public static bool CheckGuest(this UmbracoContext context, string enventId, string guestId)
        {
            var eventItem = Current.UmbracoContext.ContentCache.GetById(new Guid(enventId));
            var guestsItem = eventItem?.Children.FirstOrDefault(x => x.IsDocumentType("guests"));
            var guest = guestsItem?.Children.FirstOrDefault(x => x.Value<string>("guestId").Equals(guestId) && x.Value<bool>("guestActive"));
            return guest == null ? false : true;
        }

        public static List<Comment> Comments(this UmbracoContext context, string enventId)
        {
            var eventItem = Current.UmbracoContext.ContentCache.GetById(new Guid(enventId));
            var commentsItem = eventItem?.Children.FirstOrDefault(x => x.IsDocumentType("comments"));
            var result = new List<Comment>();
            foreach (var comment in commentsItem.Children)
            {
                var ob = new Comment();
                ob.GuestId = comment.Value<string>("guestId");
                ob.Message = comment.Value<string>("message");
                ob.GuestName = comment.Value<string>("guestName");
                ob.CommentId = comment.Value<string>("commentId");
                ob.ReplyId = comment.Value<string>("replyId");
                ob.Reply = new List<Comment>();
                if (comment.Children.Any())
                {
                    foreach (var reply in comment.Children)
                    {
                        var rep = new Comment();
                        rep.GuestId = reply.Value<string>("guestId");
                        rep.Message = reply.Value<string>("message");
                        rep.GuestName = reply.Value<string>("guestName");
                        rep.CommentId = reply.Value<string>("commentId");
                        rep.ReplyId = reply.Value<string>("replyId");
                        rep.Reply = new List<Comment>();
                        ob.Reply.Add(rep);
                    }
                }

                result.Add(ob);
            }

            return result;
        }
    }
}