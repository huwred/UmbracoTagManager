using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Cms.Web.BackOffice.Trees;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Extensions;

namespace Umbraco_Tag_Manager.Controllers
{
    [PluginController(ConstantValues.PluginAlias)]
    [Tree(ConstantValues.SectionAlias, ConstantValues.TreeAlias, TreeGroup = ConstantValues.TreeGroup)]
    public class TagManagerTreeController : TreeController
    {
        private readonly TagManagerApiController _tManagerController;
        private readonly IMenuItemCollectionFactory _menuItemCollectionFactory;

        public TagManagerTreeController(ILocalizedTextService localizedTextService, 
            UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection, 
            IEventAggregator eventAggregator,
            IScopeProvider scopeProvider,
            IContentService contentService,ILogger<TagManagerApiController> logger,IMediaService mediaService,ITagService tagService,IMenuItemCollectionFactory menuItemCollectionFactory) : base(localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
            _tManagerController = new TagManagerApiController(scopeProvider,contentService,logger,mediaService,tagService);
            _menuItemCollectionFactory = menuItemCollectionFactory ?? throw new ArgumentNullException(nameof(menuItemCollectionFactory));
        }
        protected override ActionResult<TreeNodeCollection> GetTreeNodes(string id, FormCollection queryStrings)
        {
            if (id == Constants.System.Root.ToInvariantString())
            {
                //top level nodes - generate list of tag groups that this user has access to.       
                var tree = new TreeNodeCollection();
                foreach (var tagGroup in _tManagerController.GetTagGroups())
                {
                    var item = CreateTreeNode("tagGroup-" + tagGroup.Group, id, null, tagGroup.Group, "icon-bulleted-list", true, queryStrings.GetValue<string>("application"));
                    tree.Add(item);
                }

                return tree;
            }
            else
            {
                //List all tags under group

                //Get tag groupname
                var groupName = id.Substring(id.IndexOf('-') + 1);

                var tree = new TreeNodeCollection();

                var cmsTags = _tManagerController.GetAllTagsInGroup(groupName);

                foreach (var tag in cmsTags)
                {
                    var item = CreateTreeNode(tag.Id.ToString(), groupName, queryStrings,
                        $"{tag.Tag} ({tag.NoTaggedNodes.ToString()})", "icon-bulleted-list", false);
                    tree.Add(item);
                }

                return tree;
            }
        }

        protected override ActionResult<MenuItemCollection> GetMenuForNode(string id, FormCollection queryStrings)
        {
            var menu = _menuItemCollectionFactory.Create();

            if (id.Contains("tag-"))
            {
                menu.Items.Add(new MenuItem("delete", "Delete"));
            }

            return menu;
        }
    }
}