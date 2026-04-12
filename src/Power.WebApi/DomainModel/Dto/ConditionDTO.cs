using Newtonsoft.Json;

namespace Power.WebApi.DomainModel.Dto
{
	public record ConditionDTO
	{
		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("icon")]
		public string Icon { get; set; }

		[JsonProperty("code")]
		public int Code { get; set; }
	}
}
