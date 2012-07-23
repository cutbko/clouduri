using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web.Mvc;

namespace CloudUri.Web.HtmlHelpers
{
    /// <summary>
    /// Simple class that inherits TagBuilder and allow to save sub tags.
    /// </summary>
    public class MultiLevelHtmlTag : TagBuilder
    {
        /// <summary>
        /// List of inner tags.
        /// </summary>
        private readonly IList<MultiLevelHtmlTag> _innerTags = new List<MultiLevelHtmlTag>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiLevelHtmlTag"/> class.
        /// </summary>
        /// <param name="tagName">The name of the tag.</param>
        public MultiLevelHtmlTag(string tagName)
            : base(tagName)
        {
        }

        /// <summary>
        /// Gets the inner tag list.
        /// </summary>
        /// <value>The inner tag list.</value>
        public IEnumerable<MultiLevelHtmlTag> InnerTags
        {
            get
            {
                return new ReadOnlyCollection<MultiLevelHtmlTag>(_innerTags);
            }
        }

        /// <summary>
        /// Adds the specified tag to the inner tag list.
        /// </summary>
        /// <param name="tag">The tag to add.</param>
        public void Add(MultiLevelHtmlTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException("tag");
            }

            _innerTags.Add(tag);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            if (_innerTags.Count > 0)
            {
                var sb = new StringBuilder();

                foreach (var tag in _innerTags)
                {
                    sb.AppendLine(tag.ToString());
                }

                InnerHtml = sb.ToString();
            }

            return base.ToString();
        }
    }
}