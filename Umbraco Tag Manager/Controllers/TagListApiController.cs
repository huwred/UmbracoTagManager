using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.BackOffice.Controllers;

namespace Umbraco_Tag_Manager.Controllers
{
    public class CommonTagListController : UmbracoAuthorizedApiController
    {
        private readonly ITagQuery _tagQuery;
        private readonly IContentService _contentService;

        public CommonTagListController(ITagQuery tagQuery,IContentService contentService)
        {
            _tagQuery = tagQuery;
            _contentService = contentService;
        }
        [HttpGet]
        public IEnumerable<TagModel> GetTags(string group, int max)
        {
            return _tagQuery.GetAllTags(group).OrderByDescending(t=>t.NodeCount).Take(max);
        }

        [HttpPost]
        public IContent AddTags(SelectedTag Tags)
        {
            var content = _contentService.GetById(Tags.Id);

            if (content != null)
            {
                var currentTags = JsonConvert.DeserializeObject<string[]>(content.GetValue<string>(Tags.PropertyAlias));
                string[] newTagsToSet = Tags.Tags.Select(t=>t.Text).ToArray();

                // set the tags
                string jsonTags = JsonConvert.SerializeObject(newTagsToSet.Union(currentTags).ToArray(),Formatting.None);
                content.SetValue(Tags.PropertyAlias, jsonTags);
                _contentService.Save(content);
            }

            //_contentService.SaveAndPublish(content);

            return content;
        }
    }
}