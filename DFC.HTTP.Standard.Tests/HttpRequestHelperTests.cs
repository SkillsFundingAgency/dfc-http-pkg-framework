using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using NSubstitute;
using NUnit.Framework;

namespace DFC.HTTP.Standard.Tests
{
    public class HttpRequestHelperTests
    {
        private IHttpRequestHelper _httpRequestHelper;
        private HttpRequest _request;

        [SetUp]
        public void Setup()
        {
            _httpRequestHelper = Substitute.For<HttpRequestHelper>();

            _request = new DefaultHttpRequest(new DefaultHttpContext())
            {
                ContentType = ContentApplicationType.ApplicationJSON,
            };
        }
        
        [Test]
        public void HttpRequestHelpers_SetContentTypeToApplicationJson_ThrowsArgumentNullException_WhenRequestIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _httpRequestHelper.SetContentTypeToApplicationJson(null));
        }

        [Test]
        public void HttpRequestHelpers_UpdatesContentType_WhenSetContentTypeToApplicationJsonIsCalled()
        {
            _request.ContentType = ContentApplicationType.ApplicationXML;
            _httpRequestHelper.SetContentTypeToApplicationJson(_request);
            Assert.AreEqual(_request.ContentType, ContentApplicationType.ApplicationJSON);
        }

        [Test]
        public void HttpRequestHelpers_GetQueryString_ThrowsArgumentNullException_WhenRequestIsNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _httpRequestHelper.GetQueryString(null, string.Empty));
        }

        [Test]
        public void HttpRequestHelpers_GetQueryString_ReturnsEmptyString_WhenQueryStringCannotBeFound()
        {
           var result = _httpRequestHelper.GetQueryString(_request, "Test");
            Assert.AreEqual(result, string.Empty);
        }

        [Test]
        public void HttpRequestHelpers_GetQueryString_ReturnsQueryString_WhenQueryStringExists()
        {
            _request.QueryString= new QueryString("?GivenName=John");

            var result = _httpRequestHelper.GetQueryString(_request, "GivenName");
            Assert.AreEqual(result, "John");
        }

        [Test]
        public void HttpRequestHelpers_GetHeader_ThrowsArgumentNullException_WhenRequestIsNull()
        {
            Assert.That(() => _httpRequestHelper.GetHeader(null, string.Empty),
                Throws.Exception
                    .TypeOf<ArgumentNullException>());
        }

        [Test]
        public void HttpRequestHelpers_GetHeader_ReturnsEmptyString_WhenHeaderDoesNotExist()
        {
            var result = _httpRequestHelper.GetHeader(_request, "Header");
            Assert.AreEqual(result, string.Empty);
        }

        [Test]
        public void HttpRequestHelpers_GetHeader_ReturnsHeaderValue_WhenHeaderExists()
        {
            _request.Headers.TryAdd("Number", "0123456789");
            var result = _httpRequestHelper.GetHeader(_request, "Number");
            Assert.AreEqual(result, "0123456789");
        }


        [Test]
        public void HttpRequestHelpers_GetDssTouchpointId_ThrowsArgumentNullException_WhenRequestIsNull()
        {
            Assert.That(() => _httpRequestHelper.GetDssTouchpointId(null),
                Throws.Exception
                    .TypeOf<ArgumentNullException>());
        }

        [Test]
        public void HttpRequestHelpers_GetDssTouchpointId_ReturnsEmptyString_WhenHeaderDoesNotExist()
        {
            var result = _httpRequestHelper.GetDssTouchpointId(_request);
            Assert.AreEqual(result, string.Empty);
        }

        [Test]
        public void HttpRequestHelpers_GetDssTouchpointId_ReturnsTouchpointId_WhenHeaderExists()
        {
            _request.Headers.TryAdd("TouchpointId", "0123456789");
            var result = _httpRequestHelper.GetDssTouchpointId(_request);
            Assert.AreEqual(result, "0123456789");
        }


        [Test]
        public void HttpRequestHelpers_GetDssCorrelationId_ThrowsArgumentNullException_WhenRequestIsNull()
        {
            Assert.That(() => _httpRequestHelper.GetDssTouchpointId(null),
                Throws.Exception
                    .TypeOf<ArgumentNullException>());
        }

        [Test]
        public void HttpRequestHelpers_GetDssCorrelationId_ReturnsEmptyString_WhenHeaderDoesNotExist()
        {
            var result = _httpRequestHelper.GetDssTouchpointId(_request);
            Assert.AreEqual(result, string.Empty);
        }

        [Test]
        public void HttpRequestHelpers_GetDssCorrelationId_ReturnsDssCorrelationId_WhenHeaderExists()
        {
            var correlationId = Guid.NewGuid().ToString();
            _request.Headers.TryAdd("DssCorrelationId", correlationId);
            var result = _httpRequestHelper.GetDssCorrelationId(_request);
            Assert.AreEqual(result, correlationId);
        }

        [Test]
        public void HttpRequestHelpers_GetDssSubcontractorId_ThrowsArgumentNullException_WhenRequestIsNull()
        {
            Assert.That(() => _httpRequestHelper.GetDssSubcontractorId(null),
                Throws.Exception
                    .TypeOf<ArgumentNullException>());
        }

        [Test]
        public void HttpRequestHelpers_GetDssSubcontractorId_ReturnsEmptyString_WhenHeaderDoesNotExist()
        {
            var result = _httpRequestHelper.GetDssSubcontractorId(_request);
            Assert.AreEqual(result, string.Empty);
        }

        [Test]
        public void HttpRequestHelpers_GetDssSubcontractorId_ReturnsDssSubcontractorId_WhenHeaderExists()
        {
            _request.Headers.TryAdd("SubcontractorId", "01234567890123456789");
            var result = _httpRequestHelper.GetDssSubcontractorId(_request);
            Assert.AreEqual(result, "01234567890123456789");
        }

        [Test]
        public void HttpRequestHelpers_GetDssApimURL_ThrowsArgumentNullException_WhenRequestIsNull()
        {
            Assert.That(() => _httpRequestHelper.GetDssApimUrl(null),
                Throws.Exception
                    .TypeOf<ArgumentNullException>());
        }

        [Test]
        public void HttpRequestHelpers_GetDssApimURL_ReturnsEmptyString_WhenHeaderDoesNotExist()
        {
            var result = _httpRequestHelper.GetDssApimUrl(_request);
            Assert.AreEqual(result, string.Empty);
        }

        [Test]
        public void HttpRequestHelpers_GetDssApimURL_ReturnsApimUrl_WhenHeaderExists()
        {
            _request.Headers.TryAdd("apimurl", "http://localhost");
            var result = _httpRequestHelper.GetDssApimUrl(_request);
            Assert.AreEqual(result, "http://localhost");
        }


    }
}